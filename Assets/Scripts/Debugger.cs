using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    private TMP_Text _frameRateText;
    private Button _frameRateButton;

    public static Debugger Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        _frameRateButton = transform.Find("DebugButton").GetComponent<Button>();
        _frameRateText = transform.Find("FrameRateText").GetComponent<TMP_Text>();
    }

    public void EnableFrameRateCounter()
    {
        _frameRateText.enabled = true;
        _frameRateButton.gameObject.SetActive(false);
    }
      
    void Update()
    {
        _frameRateText.text = "FPS: " + (int)(1f / Time.unscaledDeltaTime);

        if (SceneManager.GetActiveScene().name != "Title" && _frameRateText.enabled == false) // Destroy Instance if framerate counter was not enabled
        {
            Destroy(gameObject);
        }
    }
}
