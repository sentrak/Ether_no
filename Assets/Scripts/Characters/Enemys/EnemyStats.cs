using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyStats : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip death, getHit, run; // Clips de audio para la muerte y daño del enemigo

    [Header("Enemy Stats")]
    public float knockBackForceX; // Fuerza de retroceso en X
    public float knockBackForceY; // Fuerza de retroceso en Y
    private Rigidbody2D rb;
    private Enemy enemy;
    private DropManager dropManager;
    /*
        * Método: Start.
        * Parámetros: Ninguno.
        * Descripción: Inicializa las referencias necesarias
        */
    void Start()
    {
        enemy = GetComponent<Enemy>();
        dropManager = GetComponent<DropManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Validar la vida del enemigo
     */
    void Update()
    {
        DeathEnemy();
    }
    void DeathEnemy()
    {
        if (enemy.healtPoints <= 0)
        {
            AudioManager.Instance.PlaySound(death);
            enemy.healtPoints = 0;
            dropManager.DropItem();
            Destroy(gameObject);
        }
    }
    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider que interactúa con el jugador.
     * Descripción: Detecta colisiones con enemigos, aplica daño y retroceso.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerPunch"))
        {
            AudioManager.Instance.PlaySound(getHit);
            enemy.healtPoints -= 10;
            print("Te golpearon por puto");
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
            print("fuiste bendecido alv");
            enemy.healtPoints -= 20;
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


