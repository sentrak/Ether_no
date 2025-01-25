using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Clase: EnemyStats.
 * Descripción: Gestiona las estadísticas del enemigo, como la vida, el retroceso y la interacción con los ataques del jugador.
 * Maneja eventos como la muerte del enemigo, incluyendo la reproducción de sonidos, animaciones y generación de drops.
 */
public class EnemyStats : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip death; // Clip de audio para la muerte del enemigo
    [SerializeField] private AudioClip getHit; // Clip de audio para el daño recibido por el enemigo

    [Header("Enemy Stats")]
    [SerializeField] private float knockBackForceX; // Fuerza de retroceso en el eje X
    [SerializeField] private float knockBackForceY; // Fuerza de retroceso en el eje Y

    [Header("Enemy Components")]
    private Rigidbody2D rb; // Referencia al Rigidbody2D del enemigo
    private Enemy enemy; // Referencia al script Enemy que contiene las estadísticas básicas del enemigo
    private Animator animator; // Referencia al Animator para manejar las animaciones del enemigo
    private DropManager dropManager; // Referencia al DropManager para gestionar los drops al morir el enemigo

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias necesarias, como Rigidbody2D, Animator, Enemy y DropManager.
     */
    private void Start()
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
    private void Update()
    {
        DeathEnemy();
    }

    /*
     * Método: DeathEnemy.
     * Parámetros: Ninguno.
     * Descripción: Gestiona la muerte del enemigo, activa la animación de muerte, reproduce sonidos y genera drops.
     */
    private void DeathEnemy()
    {
        if (enemy != null && enemy.healtPoints <= 0)
        {
            AudioManager.Instance.PlaySound(death);
            enemy.healtPoints = 0;
            animator.SetTrigger("dead");
            dropManager?.DropItem();
            Destroy(gameObject);
        }
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider que interactúa con el enemigo.
     * Descripción: Detecta colisiones con ataques del jugador, aplica daño y retroceso, y reproduce sonidos de impacto.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerPunch"))
        {
            ApplyDamage(10, collision);
        }
        else if (collision.CompareTag("PlayerCross"))
        {
            ApplyDamage(15, collision);
        }
    }

    /*
     * Método: ApplyDamage.
     * @param damage: Cantidad de daño a aplicar.
     * @param collision: Collider del objeto atacante.
     * Descripción: Aplica daño al enemigo, reproduce el sonido de impacto y aplica retroceso en función de la posición del atacante.
     */
    private void ApplyDamage(int damage, Collider2D collision)
    {
        if (enemy != null)
        {
            AudioManager.Instance.PlaySound(getHit);
            enemy.healtPoints -= damage;

            float knockBackDirection = collision.transform.position.x > transform.position.x ? -1 : 1;
            rb.AddForce(new Vector2(knockBackDirection * knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }
    }
}
