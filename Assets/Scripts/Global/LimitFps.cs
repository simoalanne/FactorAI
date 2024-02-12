using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFps : MonoBehaviour
{
    [SerializeField]
    private int targetFps = 90;

    void Start()
    {
        Application.targetFrameRate = targetFps;
    }
}

