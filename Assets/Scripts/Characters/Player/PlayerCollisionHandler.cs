using UnityEngine;

/*
 * Clase: PlayerCollisionHandler.
 * Descripción: Gestiona las colisiones del jugador con elementos peligrosos en el juego, como púas, y maneja eventos como la reproducción
 *              de sonidos de muerte y la lógica asociada a la muerte del jugador.
 */
public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("Game Over Settings")]
    [SerializeField] private AudioClip deathSound; // Clip de audio que se reproduce al morir el jugador

    [Header("Player Components")]
    private AudioSource audioSource; // Referencia al AudioSource del jugador para reproducir sonidos
    private PlayerStats playerStats; // Referencia al script PlayerStats para gestionar la muerte del jugador

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias al AudioSource y al PlayerStats del jugador.
     */
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerStats = GetComponent<PlayerStats>();
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider del objeto que entra en contacto con el jugador.
     * Descripción: Detecta colisiones con objetos peligrosos, reproduce un sonido 
     * de muerte y ejecuta la lógica de muerte del jugador.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes"))
        {
            if (deathSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(deathSound);
            }
            if (playerStats != null)
            {
                playerStats.Die();
            }
        }
    }
}
