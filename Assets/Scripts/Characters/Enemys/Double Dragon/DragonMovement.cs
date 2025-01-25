using UnityEngine;

/*
 * Clase: DragonMovement.
 * Descripción: Controla el movimiento lateral del dragón dentro de un rango predefinido, alternando entre 
 * las direcciones izquierda y derecha. Cambia de dirección e invierte la escala al alcanzar los límites.
 */
public class DragonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f; // Velocidad del movimiento lateral
    [SerializeField] private float leftBoundary = -5f; // Límite izquierdo del movimiento
    [SerializeField] private float rightBoundary = 5f; // Límite derecho del movimiento
    [SerializeField] private bool startMovingRight = true; // Indica si el dragón comienza moviéndose hacia la derecha

    [Header("Internal State")]
    private bool movingRight; // Indica la dirección actual del movimiento

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa el estado de movimiento del dragón según el valor configurado en el Inspector.
     */
    private void Start()
    {
        movingRight = startMovingRight;
    }

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Actualiza el movimiento del dragón en cada frame.
     */
    private void Update()
    {
        MoveDragon();
    }

    /*
     * Método: MoveDragon.
     * Parámetros: Ninguno.
     * Descripción: Maneja el movimiento continuo del dragón. Cambia de dirección al 
     * alcanzar los límites y ajusta la posición según la velocidad configurada.
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
     * Descripción: Invierte la escala horizontal del dragón para simular que gira hacia la nueva dirección.
     */
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
