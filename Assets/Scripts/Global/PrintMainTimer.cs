using UnityEngine;
using TMPro;

public class PrintMainTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MainTimerDisplay;

    void Update()
    {
        MainTimerDisplay.text = MainTimer.Instance.PrintTime;        
    }
}
