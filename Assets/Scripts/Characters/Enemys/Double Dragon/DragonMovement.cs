using UnityEngine;

/*
 * Clase: DragonMovement.
 * Descripción: Maneja el movimiento lateral continuo del dragón de izquierda a derecha o viceversa,
 *              dentro de un rango definido. Invierte la dirección y la escala del dragón al alcanzar los límites.
 */
public class DragonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f; // Velocidad del movimiento lateral
    [SerializeField] private float leftBoundary = -5f; // Límite izquierdo del movimiento
    [SerializeField] private float rightBoundary = 5f; // Límite derecho del movimiento
    [SerializeField] private bool startMovingRight = true; // Indica si el dragón debe empezar moviéndose a la derecha

    private bool movingRight; // Estado interno que controla la dirección actual del movimiento

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa la dirección del movimiento según la configuración inicial.
     */
    private void Start()
    {
        movingRight = startMovingRight;
    }

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Actualiza el movimiento lateral del dragón en cada frame.
     */
    void Update()
    {
        MoveDragon();
    }

    /*
     * Método: MoveDragon.
     * Parámetros: Ninguno.
     * Descripción: Controla el movimiento del dragón entre los límites izquierdo y derecho.
     *              Cambia de dirección al alcanzar los límites definidos.
     */
    private void MoveDragon()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            if (transform.position.x >= rightBoundary)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            if (transform.position.x <= leftBoundary)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    /*
     * Método: Flip.
     * Parámetros: Ninguno.
     * Descripción: Invierte la escala del dragón para simular que cambia de dirección.
     */
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
