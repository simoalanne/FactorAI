using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private string _currentSceneName;
    private string _nextSceneName;
    private const string loadingSceneName = "LoadingScene";
    private Fader _fader;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("SceneLoader Awake");
    }

    public void StartLoadingProcess(string newSceneName)
    {
        Time.timeScale = 0.5f; // Debugging purposes
        _currentSceneName = SceneManager.GetActiveScene().name;
        _nextSceneName = newSceneName;
        SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive).completed += LoadLoadingScene_completed;
        Debug.Log("Loading new scene: " + loadingSceneName);
    }

    private void LoadLoadingScene_completed(AsyncOperation obj)
    {
        _fader = FindObjectOfType<Fader>();
        StartCoroutine(FadeAndUnload());
        Debug.Log("Loading scene completed");
    }

    private IEnumerator FadeAndUnload()
    {
        yield return _fader.FadeToBlack();
        Debug.Log("Fade to black completed");

        // Start loading the new scene additively and asynchronously
        var loadOperation = SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Additive);
        yield return loadOperation;

        if (loadOperation.isDone)
        {
            Debug.Log("New scene loaded: " + _nextSceneName);

            // Unload the current scene
            var unloadOperation = SceneManager.UnloadSceneAsync(_currentSceneName);
            yield return unloadOperation;

            if (unloadOperation.isDone)
            {
                Debug.Log("Unloading current scene: " + _currentSceneName);

                // Set the new scene as active
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(_nextSceneName));
                StartCoroutine(FadeFromBlackAndUnloadLoadingScene());
            }
        }
    }
    private void UnloadScene_completed(AsyncOperation obj)
    {
        Debug.Log("Current scene unloaded");
    }

    private void LoadNewScene_completed(AsyncOperation obj)
    {
        // Set the new scene as active
        Debug.Log("New scene set as active: " + _nextSceneName);

        StartCoroutine(FadeFromBlackAndUnloadLoadingScene());
        Debug.Log("New scene loaded: " + _nextSceneName);
    }


    private IEnumerator FadeFromBlackAndUnloadLoadingScene()
    {
        yield return _fader.FadeFromBlack();
        Debug.Log("Fade from black completed");


        Debug.Log("Loading scene unloaded");

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_nextSceneName));
        SceneManager.UnloadSceneAsync("LoadingScene");

        Debug.Log("Ready to play new scene: " + _nextSceneName);
    }
}