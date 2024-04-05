using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaler : MonoBehaviour
{
    [SerializeField] private float originalAspectRatio = 16f / 9f;
    private RectTransform _rectTransform;

    void Start()
    {
        float currentAspectRatio = (float)Screen.height / Screen.width;
        float scaleFactor = currentAspectRatio / originalAspectRatio;
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localScale *= new Vector2(1, scaleFactor);
        _rectTransform.anchoredPosition *= new Vector2(1, scaleFactor);
    }
}
