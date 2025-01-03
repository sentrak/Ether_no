using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource, musicAudioSource;
    private bool isMusicPlaying;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Validar que los AudioSources están asignados
        if (sfxAudioSource == null || musicAudioSource == null)
        {
            Debug.LogError("AudioManager: Uno o más AudioSource no están asignados en el Inspector.");
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (sfxAudioSource != null && clip != null)
        {
            sfxAudioSource.PlayOneShot(clip);
        }
        else if (clip == null)
        {
            Debug.LogWarning("AudioManager: El AudioClip pasado a PlaySound es null.");
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicAudioSource != null && clip != null)
        {
            if (musicAudioSource.isPlaying)
            {
                musicAudioSource.Stop();
            }
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
            isMusicPlaying = true;
        }
        else if (clip == null)
        {
            Debug.LogWarning("AudioManager: El AudioClip pasado a PlayMusic es null.");
        }
    }

    public void StopMusic()
    {
        if (musicAudioSource != null && musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
            isMusicPlaying = false;
        }
    }

    public bool GetIsMusicPlaying()
    {
        return isMusicPlaying;
    }

    public void SetMusicVolume(float volume)
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = Mathf.Clamp01(volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = Mathf.Clamp01(volume);
        }
    }
}
