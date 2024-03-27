using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button _button;

    private readonly float pulseSpeed = 0.25f;

    private Vector3 originalScale;
    private bool isButtonPressed = false;

    void Start()
    {
        _button = GetComponent<Button>();
        originalScale = _button.transform.localScale;
    }

    void Update()
    {
        float scaleFactor;
        if (isButtonPressed)
        {
            scaleFactor = Mathf.PingPong(Time.time * pulseSpeed, 0.1f) + 0.9f;
        }
        else
        {
            scaleFactor = Mathf.PingPong(Time.time * pulseSpeed, 0.1f) + 1f;
        }
        _button.transform.localScale = originalScale * scaleFactor;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
    }
}