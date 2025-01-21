using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Clase: EnemyStats.
 * Descripción: Gestiona las estadísticas y eventos relacionados con el enemigo, como la validación de su vida,
 * la ejecución de animaciones de muerte y la generación de drops al morir.Esta clase se encarga de la interacción
 * principal entre el jugador y el enemigo, incluyendo el daño y el retroceso.
 */
public class EnemyStats : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip death, getHit; // Clips de audio para la muerte y daño del enemigo

    [Header("Enemy Stats")]
    public float knockBackForceX; // Fuerza de retroceso en X
    public float knockBackForceY; // Fuerza de retroceso en Y

    private Rigidbody2D rb; // Referencia al Rigidbody2D del enemigo
    private Enemy enemy; // Referencia al script Enemy que contiene las estadísticas del enemigo
    private Animator animator; // Referencia al Animator para manejar las animaciones del enemigo
    private DropManager dropManager; // Referencia al DropManager para gestionar los drops al morir el enemigo

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias necesarias, incluyendo el Rigidbody2D, el Animator, el script Enemy y el DropManager.
     */
    void Start()
    {
        enemy = GetComponent<Enemy>();
        dropManager = GetComponent<DropManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
    }

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Valida constantemente la vida del enemigo y ejecuta la lógica de muerte si su vida llega a 0.
     */
    void Update()
    {
        DeathEnemy();
    }

    /*
     * Método: DeathEnemy.
     * Parámetros: Ninguno.
     * Descripción: Gestiona la muerte del enemigo, reproduce los efectos de sonido, activa la animación de muerte y genera drops.
     */
    void DeathEnemy()
    {
        if (enemy.healtPoints <= 0)
        {
            AudioManager.Instance.PlaySound(death);
            enemy.healtPoints = 0;
            animator.SetTrigger("dead");
            dropManager.DropItem();
            Destroy(gameObject);
        }
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider que interactúa con el enemigo.
     * Descripción: Detecta colisiones con los ataques del jugador, aplica daño, retroceso y reproduce sonidos de impacto.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerPunch"))
        {
            AudioManager.Instance.PlaySound(getHit);
            enemy.healtPoints -= 10;
            if (collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);
            }
        }
        if (collision.CompareTag("PlayerCross"))
        {
            AudioManager.Instance.PlaySound(getHit);
            enemy.healtPoints -= 15;
            if (collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);
            }
        }
    }
}
