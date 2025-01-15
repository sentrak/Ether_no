using UnityEngine;

public class CameraControllerWithVerticalMovement : MonoBehaviour
{
    [Header("Player and Room Settings")]
    public Transform player; // Referencia al Transform del jugador
    public Transform activeRoom; // Referencia al Transform de la habitación activa que define los límites

    [Header("Camera Modifiers")]
    [Range(-5, 5)] public float minModX, maxModX, minModY, maxModY; // Modificadores para ajustar los límites de la cámara

    [Header("Vertical Movement Settings")]
    [SerializeField] private float amplitude = 2f; // Amplitud del movimiento vertical, define la distancia máxima desde la posición inicial
    [SerializeField] private float speed = 2f; // Velocidad del movimiento oscilatorio en el eje Y

    private Vector3 startPosition; // Almacena la posición inicial del GameObject

    public static CameraControllerWithVerticalMovement instance; // Instancia singleton de la cámara con movimiento vertical

    /*
     * Método: Awake.
     * Parámetros: Ninguno.
     * Descripción: Configura el singleton de la cámara y almacena la posición inicial para el movimiento vertical.
     */
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        startPosition = transform.position; // Guardar la posición inicial para el movimiento vertical
    }

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Combina el seguimiento del jugador con el movimiento vertical oscilatorio.
     */
    void Update()
    {
        // Validar referencias
        if (activeRoom == null || player == null)
        {
            Debug.LogError("La referencia a 'activeRoom' o 'player' no está asignada en el CameraControllerWithVerticalMovement.");
            return;
        }

        // Obtener los límites del BoxCollider2D de la habitación activa
        BoxCollider2D roomBounds = activeRoom.GetComponent<BoxCollider2D>();
        if (roomBounds == null)
        {
            Debug.LogError("El 'activeRoom' no tiene un BoxCollider2D asignado.");
            return;
        }

        // Calcular los límites de la cámara con los modificadores
        float minPosY = roomBounds.bounds.min.y + minModY;
        float maxPosY = roomBounds.bounds.max.y + maxModY;
        float minPosX = roomBounds.bounds.min.x + minModX;
        float maxPosX = roomBounds.bounds.max.x + maxModX;

        // Movimiento vertical oscilatorio
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        // Limitar la posición de la cámara dentro de los límites calculados
        Vector3 clampedPos = new Vector3(
            Mathf.Clamp(player.position.x, minPosX, maxPosX),
            Mathf.Clamp(newY, minPosY, maxPosY), // Combinar el movimiento vertical con el límite Y
            transform.position.z // Mantener la posición Z fija
        );

        // Aplicar la posición combinada
        transform.position = clampedPos;
    }
}
