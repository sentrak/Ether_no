using UnityEngine;

/*
 * Clase: ContextWorld.
 * Descripción: Gestiona el contexto del mundo del juego, como la reproducción de música de fondo al iniciar la escena.
 */
public class ContextWorld : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip levelMusic; // Música de fondo para el nivel actual

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Reproduce la música de fondo asignada al iniciar la escena.
     */
    private void Start()
    {
        if (levelMusic != null)
        {
            AudioManager.Instance.PlayMusic(levelMusic);
        }
    }
}
