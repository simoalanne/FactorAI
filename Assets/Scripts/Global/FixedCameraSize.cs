using UnityEngine;

namespace Global
{
    public class CameraSizeAdjuster : MonoBehaviour
    {
        public float originalAspectRatio = 16f / 9f; // Alkuperäinen näyttökuvasuhde
        public float originalCameraSize = 14f; // Alkuperäinen kameran koko

        private void Start()
        {
            Camera.main.orthographicSize = originalCameraSize;
            float currentAspectRatio = (float)Screen.height / Screen.width;
            Debug.Log("screen width:" + Screen.width);
            Debug.Log("screen height:" + Screen.height);
            Debug.Log("current aspect:" + currentAspectRatio);
            float scaleFactor = currentAspectRatio / originalAspectRatio;
            Debug.Log("scale factor:" + scaleFactor);
            float newCameraSize = originalCameraSize * scaleFactor;
            Debug.Log("new camera size:" + newCameraSize);
            Camera.main.orthographicSize = newCameraSize;
            Debug.Log("camera size set");
        }
    }
}
