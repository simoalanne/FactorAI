using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource; // Assign your Audio Source in the Inspector

    void Start()
    {
        audioSource.Play(); // Start playing the background music
    }
}