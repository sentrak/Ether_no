using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Camera Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private float smoothSpeed = 0.125f;

    [Header("Boundaries (Optional)")]
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("No se ha asignado un jugador al script CameraFollow.");
            return;
        }


        Vector3 desiredPosition = player.position + offset;


        desiredPosition.y = player.position.y + offset.y;


        if (minBounds != Vector2.zero && maxBounds != Vector2.zero)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);
        }


        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);


        transform.position = smoothedPosition;
    }
}
