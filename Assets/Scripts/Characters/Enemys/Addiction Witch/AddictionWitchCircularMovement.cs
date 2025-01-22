using System.Collections;
using UnityEngine;

/*
 * Clase: AddictionWitchCircularMovement.
 * Descripción: Gestiona el comportamiento de la "Addiction Witch", incluyendo mantener una distancia constante con el jugador,
 * realizar un movimiento circular y reproducir un audio cada 8 segundos.
 */
public class AddictionWitchCircularMovement : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private Transform player; // Referencia al jugador

    [Header("Movement Settings")]
    [SerializeField] private float distanceFromPlayer = 5f; // Distancia constante con el jugador
    [SerializeField] private float orbitRadius = 3f; // Radio del movimiento circular
    [SerializeField] private float orbitSpeed = 2f; // Velocidad del movimiento circular
    [SerializeField] private float upY = 2f; // Desplazamiento adicional en el eje Y

    [Header("Audio Settings")]
    [SerializeField] private AudioClip witchLaught; // Clip de audio de la risa de la bruja
    public bool isAnimating { get; set; } = false; // Indica si el jugador está usando una habilidad

        private float angle; // Ángulo actual del movimiento circular

    void Start()
    {

    }

    void Update()
    {

        if (!isAnimating)
        {
            PerformCircularMovement();
            MaintainDistance();
            StartCoroutine(PlayWitchLaughRoutine());
        }else{
            StartCoroutine(PlayWitchLaughRoutine());
        }
    }

    /*
     * Método: MaintainDistance.
     * Descripción: Mantiene una distancia constante entre la "Addiction Witch" y el jugador.
     */
    private void MaintainDistance()
    {
        Vector3 directionToPlayer = (transform.position - player.position).normalized;
        transform.position = player.position + directionToPlayer * distanceFromPlayer + new Vector3(0, upY, 0);
    }

    /*
     * Método: PerformCircularMovement.
     * Descripción: Realiza un movimiento circular continuo basado en el radio y la velocidad configurados.
     */
    private void PerformCircularMovement()
    {
        angle += orbitSpeed * Time.deltaTime;

        float xOffset = Mathf.Cos(angle) * orbitRadius;
        float yOffset = Mathf.Sin(angle) * orbitRadius;

        transform.position = new Vector3(xOffset, yOffset, transform.position.z);

        // Reinicia el ángulo si supera los 360 grados (2 * PI en radianes)
        if (angle >= 2 * Mathf.PI)
        {
            angle -= 2 * Mathf.PI;
        }
    }

    /*
     * Método: PlayWitchLaughRoutine.
     * Descripción: Reproduce el audio de la risa de la bruja cada 8 segundos.
     */
    private IEnumerator PlayWitchLaughRoutine()
    {
        yield return new WaitForSeconds(8f);
        AudioManager.Instance.PlaySound(witchLaught);
    }

}
