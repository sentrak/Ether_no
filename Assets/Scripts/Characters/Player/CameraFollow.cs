using UnityEngine;

/*
 * Clase: CameraFollow.
 * Descripción: Gestiona el seguimiento suave de la cámara hacia el jugador, con la opción de establecer
 * límites en el movimiento. Permite configurar un desplazamiento (offset) y un suavizado en el movimiento de la cámara.
 */
public class CameraFollow : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform player; // Referencia al Transform del jugador que la cámara seguirá

    [Header("Camera Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10); // Desplazamiento de la cámara respecto al jugador
    [SerializeField] private float smoothSpeed = 0.125f; // Velocidad de suavizado del movimiento de la cámara

    [Header("Boundaries (Optional)")]
    [SerializeField] private Vector2 minBounds; // Límite mínimo (X, Y) para el movimiento de la cámara
    [SerializeField] private Vector2 maxBounds; // Límite máximo (X, Y) para el movimiento de la cámara

    /*
     * Método: LateUpdate.
     * Parámetros: Ninguno.
     * Descripción: Actualiza la posición de la cámara después de que se hayan calculado las 
     * posiciones del jugador. Aplica un desplazamiento, límites opcionales y un movimiento suavizado.
     */
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        if (minBounds != Vector2.zero && maxBounds != Vector2.zero)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);
        }
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
