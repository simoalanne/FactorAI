using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;
        Destroy(gameObject);
    }
}