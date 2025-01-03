using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float amplitude = 2f; // Altura máxima de la parábola
    [SerializeField] private float frequency = 1f; // Velocidad del movimiento horizontal
    [SerializeField] private float range = 5f; // Distancia horizontal entre los extremos

    private float direction = 1f; // Dirección del movimiento (1 = derecha, -1 = izquierda)
    private float initialX;

    private void Start()
    {
        // Guardamos la posición inicial en X
        initialX = transform.position.x;
    }

    private void Update()
    {
        /* Método: Update.
         * Parámetros: Ninguno.
         * Descripción: Mueve el GameObject siguiendo una trayectoria parabólica abierta hacia arriba.
         */

        // Actualizar posición horizontal
        float newX = transform.position.x + direction * frequency * Time.deltaTime;
        // Calcular posición vertical como una parábola
        float newY = amplitude * Mathf.Pow(newX - initialX, 2) - amplitude;
        // Aplicar nueva posición
        transform.position = new Vector3(newX, newY, transform.position.z);
        // Cambiar dirección si se alcanza el límite del rango
        if (Mathf.Abs(newX - initialX) >= range)
        {
            direction *= -1f; // Cambiar dirección
        }
    }
}
