using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 1f;
    [SerializeField] private Image _image; 

    void Start()
    {
        if (_image == null)
        {
            Debug.LogError("_image component not found");
        }
        _image.color = new Color(0, 0, 0, 0);
    }

    public IEnumerator FadeToBlack()
    {
        while (_image.color.a < 1)
        {
            _image.color = new Color(0, 0, 0, _image.color.a + Time.deltaTime * fadeSpeed);
            yield return null;
        }
    }

    public IEnumerator FadeFromBlack()
    {
        while (_image.color.a > 0)
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            _image.color = new Color(0, 0, 0, _image.color.a + Time.deltaTime * -fadeSpeed);
            yield return null;
        }
    }
}