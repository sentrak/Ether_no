using UnityEngine;

public class PlayerProximityRadar : MonoBehaviour
{
    [Header("Radar Settings")]
    [SerializeField] private float detectionRange = 10f; // Rango para detectar al jugador
    [SerializeField] private float closeRange = 2f; // Rango cercano para mostrar el mensaje en consola
    [SerializeField] private float pauseDuration = 2f; // Duración de la pausa al tocar al jugador

    private Transform player; // Referencia al Transform del jugador
    private Enemy enemy; // Referencia al Script Enemy
    private bool isFacingRight = false; // Indica si el enemigo está mirando a la derecha
    private bool isPaused = false; // Controla si el enemigo está en pausa

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Busca al jugador en la escena por su tag "Player" y referencia al script Enemy.
     */
    void Start()
    {
        enemy = GetComponent<Enemy>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el tag 'Player' en la escena.");
        }
    }

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Detecta la proximidad del jugador y mueve al enemigo si está en rango, siempre que no esté en pausa.
     */
    void Update()
    {
        if (player == null || isPaused) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer();

            if (distanceToPlayer <= closeRange)
            {
                Debug.Log("El enemigo está cerca del jugador.");
            }
        }
    }

    /*
     * Método: MoveTowardsPlayer.
     * Parámetros: Ninguno.
     * Descripción: Mueve al enemigo hacia la posición del jugador en el eje X.
     */
    private void MoveTowardsPlayer()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
        {
            Flip();
        }
        transform.position = new Vector3(
            transform.position.x + direction * enemy.moveSpeed * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
    }

    /*
     * Método: Flip.
     * Parámetros: Ninguno.
     * Descripción: Invierte la dirección en la que está mirando el enemigo.
     */
    private void Flip()
    {
        isFacingRight = !isFacingRight; 
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f; 
        transform.localScale = localScale;
    }

    /*
     * Método: OnCollisionEnter2D.
     * @param collision: Collider del objeto que entra en contacto.
     * Descripción: Maneja la pausa del enemigo al tocar al jugador.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("El enemigo ha tocado al jugador. Pausa iniciada.");
            StartCoroutine(PauseBeforeFollowing());
        }
    }

    /*
     * Método: PauseBeforeFollowing.
     * Parámetros: Ninguno.
     * Descripción: Pausa el movimiento del enemigo durante un tiempo específico antes de seguir al jugador nuevamente.
     */
    private System.Collections.IEnumerator PauseBeforeFollowing()
    {
        isPaused = true; 
        yield return new WaitForSeconds(pauseDuration); 
        isPaused = false;
        Debug.Log("El enemigo reanuda el seguimiento del jugador.");
    }
}
