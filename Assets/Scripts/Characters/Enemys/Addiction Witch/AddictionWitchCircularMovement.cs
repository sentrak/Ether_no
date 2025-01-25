using System.Collections;
using UnityEngine;

/*
 * Clase: AddictionWitchCircularMovement.
 * Descripción: Controla el movimiento de la "Addiction Witch", que realiza un movimiento circular 
 * alrededor del jugador, mantiene una distancia constante y reproduce un sonido de risa cada 8 segundos.
 */
public class AddictionWitchCircularMovement : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private Transform player; // Referencia al Transform del jugador

    [Header("Distance Settings")]
    [SerializeField] private float distanceFromPlayer = 5f; // Distancia constante que la bruja mantiene del jugador
    [SerializeField] private float upY = 2f; // Desplazamiento adicional en el eje Y para mantener la altura

    [Header("Circular Movement Settings")]
    [SerializeField] private float orbitRadius = 3f; // Radio del movimiento circular
    [SerializeField] private float orbitSpeed = 2f; // Velocidad del movimiento circular en radianes por segundo

    [Header("Audio Settings")]
    [SerializeField] private AudioClip witchLaught; // Clip de audio para la risa de la bruja

    [Header("State Management")]
    public bool isAnimating { get; set; } = false; // Indica si el jugador está usando una habilidad o la bruja está animándose

    [Header("Internal State")]
    private float angle; // Ángulo actual del movimiento circular

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Actualiza el comportamiento de la bruja en cada frame, incluyendo el movimiento 
     * circular, el mantenimiento de la distancia con el jugador y la reproducción de audio.
     */
    void Update()
    {
        if (!isAnimating)
        {
            PerformCircularMovement();
            MaintainDistance();
            StartCoroutine(PlayWitchLaughRoutine());
        }
        else
        {
            StartCoroutine(PlayWitchLaughRoutine());
        }
    }

    /*
     * Método: MaintainDistance.
     * Parámetros: Ninguno.
     * Descripción: Calcula y ajusta la posición de la bruja para mantener una distancia constante del jugador.
     */
    private void MaintainDistance()
    {
        Vector3 directionToPlayer = (transform.position - player.position).normalized;
        transform.position = player.position + directionToPlayer * distanceFromPlayer + new Vector3(0, upY, 0);
    }

    /*
     * Método: PerformCircularMovement.
     * Parámetros: Ninguno.
     * Descripción: Calcula y aplica un movimiento circular en el plano X-Y utilizando un ángulo basado en el tiempo.
     */
    private void PerformCircularMovement()
    {
        angle += orbitSpeed * Time.deltaTime;

        float xOffset = Mathf.Cos(angle) * orbitRadius;
        float yOffset = Mathf.Sin(angle) * orbitRadius;

        transform.position = new Vector3(xOffset, yOffset, transform.position.z);

        if (angle >= 2 * Mathf.PI)
        {
            angle -= 2 * Mathf.PI; 
        }
    }

    /*
     * Método: PlayWitchLaughRoutine.
     * Parámetros: Ninguno.
     * Descripción: Espera un tiempo definido (8 segundos) y reproduce un clip de audio para la risa de la bruja.
     */
    private IEnumerator PlayWitchLaughRoutine()
    {
        yield return new WaitForSeconds(8f);
        AudioManager.Instance.PlaySound(witchLaught);
    }
}
