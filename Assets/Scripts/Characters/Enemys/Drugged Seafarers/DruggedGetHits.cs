using System.Collections;
using UnityEngine;
/*
 * Clase: DruggedGetHits.
 * Descripción: Maneja la detección de golpes al enemigo, aplicando daño, animaciones y retroceso al ser impactado por el jugador.
 */
public class DruggedGetHits : MonoBehaviour
{
    [Header("Invulnerability Settings")]
    [SerializeField] private float knockBackForceX; // Fuerza de retroceso en el eje X
    [SerializeField] private float knockBackForceY; // Fuerza de retroceso en el eje Y
    private Enemy enemy; // Referencia al script Enemy en el GameObject padre
    private Animator animator; // Referencia al Animator del GameObject padre
    private Rigidbody2D rb; // Referencia al Rigidbody2D del GameObject padre
    [Header("Audio Sources")]
    [SerializeField] private AudioClip getHit; // Clip de audio reproducido al recibir un atacar 
    [SerializeField] private AudioClip getMagic; // Clip de audio reproducido al recibir un atacar 
    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias al script Enemy, Animator y Rigidbody2D desde el GameObject padre.
     */
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        rb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider del objeto que entra en contacto.
     * Descripción: Maneja el daño y el retroceso del enemigo cuando recibe un golpe.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerPunch"))
        {
            ApplyDamage(10);
            ApplyKnockback(collision);
            AudioManager.Instance.PlaySound(getHit);

        }
        else if (collision.CompareTag("PlayerCross"))
        {
            ApplyDamage(20);
            ApplyKnockback(collision);
        }
    }

    /*
     * Método: ApplyDamage.
     * @param damage: Cantidad de daño a aplicar al enemigo.
     * Descripción: Reduce los puntos de vida del enemigo y activa la animación "getHit".
     */
    private void ApplyDamage(int damage)
    {
        if (enemy != null)
        {
            enemy.healtPoints -= damage;
            animator.SetTrigger("getHit");
        }
    }

    /*
     * Método: ApplyKnockback.
     * @param collision: Collider del objeto que ocasiona el retroceso.
     * Descripción: Aplica una fuerza de retroceso al enemigo en el eje X y Y dependiendo de la posición del atacante.
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
