using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource musicSource;

    public void StopMusic()
    {
        // Smoothly stops the music for the scene transition
        musicSource.Stop();
    }
}