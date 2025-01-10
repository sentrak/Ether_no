using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform player; // Referencia al jugador

    [Header("Camera Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10); // Desplazamiento de la cámara
    [SerializeField] private float smoothSpeed = 0.125f; // Suavidad del movimiento

    [Header("Boundaries (Optional)")]
    [SerializeField] private Vector2 minBounds; // Límites mínimos de la cámara
    [SerializeField] private Vector2 maxBounds; // Límites máximos de la cámara

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("No se ha asignado un jugador al script CameraFollow.");
            return;
        }

        // Crear la posición deseada, manteniendo el offset original en X y Z
        Vector3 desiredPosition = player.position + offset;

        // Ajustar el offset en Y para que siempre esté centrado
        desiredPosition.y = player.position.y + offset.y;

        // Limitar la cámara a los límites definidos (si es necesario)
        if (minBounds != Vector2.zero && maxBounds != Vector2.zero)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);
        }

        // Suavizar el movimiento de la cámara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualizar la posición de la cámara
        transform.position = smoothedPosition;
    }
}
