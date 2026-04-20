using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public float fadeDuration = 2.0f;

    void Start()
    {
        // If you didn't drag the source in the inspector, it finds it here
        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();

        PlayMusic();
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.volume = 1f;
            musicSource.Play();
        }
    }

    // You will call this later when transporting to the "Dead Reindeer" scene
    public void StartFadeToWind()
    {
        StartCoroutine(FadeOutMusic());
    }

    private IEnumerator FadeOutMusic()
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicSource.Stop();
    }
}