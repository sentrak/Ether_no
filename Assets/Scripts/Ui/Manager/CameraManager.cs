using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float amplitude = 2f; // Distancia de movimiento vertical
    [SerializeField] private float speed = 2f; // Velocidad del movimiento

    private Vector3 startPosition;

    private void Start()
    {
        // Guardar la posición inicial del GameObject
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calcular la nueva posición en el eje Y
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        // Actualizar la posición del GameObject
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
