using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        TargetFrameRate();
    }

    private void TargetFrameRate()
    {
        Application.targetFrameRate = 120;
    }
}


    

