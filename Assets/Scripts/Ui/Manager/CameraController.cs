using UnityEngine;

/*
 * Clase: CameraControllerWithVerticalMovement.
 * Descripción: Gestiona el seguimiento del jugador mediante la cámara, combinando un movimiento oscilatorio vertical.
 * También respeta los límites definidos por la habitación activa, manteniendo la cámara dentro de un rango específico.
 */
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

    private Vector3 startPosition; // Almacena la posición inicial de la cámara

    public static CameraControllerWithVerticalMovement instance; // Instancia singleton del controlador de la cámara

    /*
     * Método: Awake.
     * Parámetros: Ninguno.
     * Descripción: Configura el patrón singleton de la cámara y almacena su posición inicial.
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

        startPosition = transform.position;
    }

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Ajusta la posición de la cámara para que siga al jugador dentro de los límites definidos,
     *              y combina el movimiento oscilatorio vertical con los desplazamientos en X e Y.
     */
    void Update()
    {
        BoxCollider2D roomBounds = activeRoom.GetComponent<BoxCollider2D>();
        float minPosY = roomBounds.bounds.min.y + minModY;
        float maxPosY = roomBounds.bounds.max.y + maxModY;
        float minPosX = roomBounds.bounds.min.x + minModX;
        float maxPosX = roomBounds.bounds.max.x + maxModX;

        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        Vector3 clampedPos = new Vector3(
            Mathf.Clamp(player.position.x, minPosX, maxPosX),
            Mathf.Clamp(newY, minPosY, maxPosY),
            transform.position.z
        );

        transform.position = clampedPos;
    }
}
