using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("Game Over Settings")]
    [SerializeField] private AudioClip deathSound; // Verificar sonido de muerte

    private AudioSource audioSource; // Para reproducir sonidos
    private PlayerStats playerStats; // Referencia al script PlayerStats

    void Start()
    {
        // Asegúrate de que el jugador tiene un AudioSource para reproducir el sonido
        audioSource = GetComponent<AudioSource>();
        playerStats = GetComponent<PlayerStats>(); // Obtener el script PlayerStats
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Colisión detectada con: {collision.gameObject.name}");
        // Verifica si el jugador tocó las púas
        if (collision.CompareTag("Spikes"))
        {
            Debug.Log("El jugador toco las puas");
            // Reproducir el sonido de muerte, se debe configurar en el inspector
            if (deathSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(deathSound);
            }

            // Llama al método de muerte en PlayerStats
            playerStats.Die();
        }
    }
}
