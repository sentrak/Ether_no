using UnityEngine;

/*
 * Clase: EnemyMovement.
 * Descripción: Controla el movimiento del enemigo, incluyendo el seguimiento del jugador, detección de rangos y la activación de animaciones.
 *              El enemigo ajusta su orientación para mirar siempre hacia el jugador.
 */
public class EnemyMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float detectionDistance = 15f; // Distancia máxima a la que el enemigo detecta al jugador
    [SerializeField] private float stopDistance = 2f; // Distancia mínima a la que el enemigo se detiene para atacar
    [SerializeField] private float moveSpeed = 5f; // Velocidad de movimiento del enemigo

    [Header("Enemy Components")]
    private Transform player; // Referencia al Transform del jugador
    private Animator animator; // Referencia al Animator del enemigo
    private DruggedAttack druggedAttack; // Referencia al script DruggedAttack del enemigo

    [Header("State Management")]
    private bool isFacingRight = true; // Indica si el enemigo está mirando a la derecha

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias al jugador, Animator y DruggedAttack.
     */
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        druggedAttack = GetComponent<DruggedAttack>();
    }

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Gestiona el movimiento del enemigo, detecta al jugador y realiza ataques cuando está en rango.
     */
    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionDistance && distanceToPlayer > stopDistance)
        {
            animator.SetBool("walk", true);
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer <= stopDistance)
        {
            animator.SetBool("walk", false);
            druggedAttack.ExecuteAttack();
        }
        else
        {
            animator.SetBool("walk", false);
        }

        FlipSprite();
    }

    /*
     * Método: MoveTowardsPlayer.
     * Parámetros: Ninguno.
     * Descripción: Mueve al enemigo hacia el jugador mientras el jugador está dentro del rango de detección.
     */
    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += new Vector3(direction.x, 0, 0) * moveSpeed * Time.deltaTime;
    }

    /*
     * Método: FlipSprite.
     * Parámetros: Ninguno.
     * Descripción: Invierte la orientación del sprite del enemigo para que siempre mire hacia el jugador.
     */
    private void FlipSprite()
    {
        if (player.position.x > transform.position.x && !isFacingRight)
        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (player.position.x < transform.position.x && isFacingRight)
        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
