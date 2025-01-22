using System.Collections;
using UnityEngine;

/*
 * Clase: PlayerGetHits.
 * Descripción: Gestiona la detección de golpes al jugador, aplicando daño, retroceso y animaciones.
 */
public class PlayerGetHits : MonoBehaviour
{
    [Header("Invulnerability Settings")]
    [SerializeField] private float knockBackForceX; // Fuerza de retroceso en el eje X al recibir daño
    [SerializeField] private float knockBackForceY; // Fuerza de retroceso en el eje Y al recibir daño
    private PlayerStats playerStats; // Referencia al script PlayerStats que maneja las estadísticas del jugador
    private Animator animator; // Referencia al Animator del jugador
    private Rigidbody2D rb; // Referencia al Rigidbody2D del jugador

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias necesarias, como PlayerStats, Rigidbody2D, Animator.
     */
    void Start()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        rb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider del objeto que interactúa con el jugador.
     * Descripción: Detecta colisiones con ataques enemigos, aplica daño y retroceso.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack"))
        {
            ApplyDamage(15);
            ApplyKnockback(collision);
        }
        if (collision.CompareTag("Fire"))
        {
            ApplyDamage(20);
            ApplyKnockback(collision);
        }
        if (collision.CompareTag("Ligthing"))
        {
            ApplyDamage(20);
            ApplyKnockback(collision);
        }
    }

    /*
     * Método: ApplyDamage.
     * @param damage: Cantidad de daño que se aplicará al jugador.
     * Descripción: Reduce los puntos de vida del jugador y activa la animación "getHit".
     */
    private void ApplyDamage(int damage)
    {
        if (playerStats != null)
        {
            playerStats.health -= damage;
            animator.SetTrigger("getHit");
        }
    }

    /*
     * Método: ApplyKnockback.
     * @param collision: Collider del objeto que ocasiona el retroceso.
     * Descripción: Aplica una fuerza de retroceso al jugador en el eje X y Y dependiendo de la posición del atacante.
     */
    private void ApplyKnockback(Collider2D collision)
    {
        if (collision.transform.position.x > rb.transform.position.x)
        {
            rb.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }
        else
        {
            rb.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }
    }
}
