using UnityEngine;

/*
 * Clase: AudioManager.
 * Descripción: Gestiona la reproducción de música y efectos de sonido (SFX) en el juego utilizando un
 *              patrón Singleton. Proporciona métodos para reproducir, detener y ajustar el volumen de música y SFX.
 */
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } // Instancia Singleton del AudioManager

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource; // AudioSource dedicado a la música de fondo
    [SerializeField] private AudioSource sfxSource;   // AudioSource dedicado a efectos de sonido (SFX)

    [Header("Loop Settings")]
    private bool isLoopEnabled = false; // Estado de loop para los efectos de sonido

    /*
     * Método: Awake.
     * Parámetros: Ninguno.
     * Descripción: Configura el patrón Singleton y asegura que el AudioManager persista entre escenas.
     */
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /*
     * Método: PlayMusic.
     * @param clip: AudioClip de la música que se reproducirá.
     * Descripción: Reproduce un AudioClip como música de fondo en bucle.
     */
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource != null && clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    /*
     * Método: StopMusic.
     * Parámetros: Ninguno.
     * Descripción: Detiene la reproducción de la música de fondo si está activa.
     */
    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    /*
     * Método: PlaySound.
     * @param clip: Clip de audio a reproducir.
     * Descripción: Reproduce un efecto de sonido con el estado de loop configurado.
     */
    public void PlaySound(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.loop = isLoopEnabled;
            sfxSource.clip = clip;
            sfxSource.Play();
        }
    }

    /*
     * Método: StopSound.
     * Parámetros: Ninguno.
     * Descripción: Detiene la reproducción de los efectos de sonido si están activos.
     */
    public void StopSound()
    {
        if (sfxSource != null && sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }

    /*
     * Método: LoopSound.
     * @param isLoop: Indica si los sonidos deben reproducirse en bucle.
     * Descripción: Configura el estado de loop para los efectos de sonido reproducidos en el futuro.
     */
    public void LoopSound(bool isLoop)
    {
        isLoopEnabled = isLoop;
        if (sfxSource != null)
        {
            sfxSource.loop = isLoop;
        }
    }

    /*
     * Método: SetMusicVolume.
     * @param volume: Nuevo volumen para la música, entre 0 y 1.
     * Descripción: Ajusta el volumen de la música de fondo.
     */
    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Clamp01(volume);
        }
    }

    /*
     * Método: SetSFXVolume.
     * @param volume: Nuevo volumen para los efectos de sonido, entre 0 y 1.
     * Descripción: Ajusta el volumen de los efectos de sonido.
     */
    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = Mathf.Clamp01(volume);
        }
    }
}
