using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip death, getHit; // Clips de audio para la muerte y daño del jugador

    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 100; // Vida máxima del jugador
    [SerializeField] private int maxMana = 50; // Maná máximo del jugador

    public float health; // Vida actual del jugador
    public float mana; // Maná actual del jugador
    public float inmunityTime; // Tiempo de inmunidad después de recibir daño
    private bool isInmune; // Indica si el jugador es inmune al daño
    public float knockBackForceX; // Fuerza de retroceso en X

    [Header("Player stats image bar")]
    public float knockBackForceY; // Fuerza de retroceso en Y
    public Image healtImg; // Imagen de barra de vida
    public Image ManaImg; // Imagen de barra de maná
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerMoviement playerMoviement;
 /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias necesarias y configura los valores iniciales de vida y maná.
     */
    void Start()
    {
        playerMoviement = GetComponent<PlayerMoviement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        health = maxHealth;
        mana = maxMana;
    }

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Actualiza las barras de vida y maná y maneja condiciones como muerte del jugador.
     */
    void Update()
    {
        healtImg.fillAmount = health / maxHealth;
        ManaImg.fillAmount = mana / maxMana;

        if (health > maxHealth) health = maxHealth;
        else if (health <= 0)
        {
            AudioManager.Instance.PlaySound(death);
            health = 0;
            SceneManager.LoadScene("05 game over");
        }

        if (mana > maxMana) mana = maxMana;
        else if (mana <= 0) mana = 0;
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider que interactúa con el jugador.
     * Descripción: Detecta colisiones con enemigos, aplica daño y retroceso.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInmune)
        {
            animator.SetTrigger("getHit");

            AudioManager.Instance.PlaySound(getHit);
            health -= 10;
            StartCoroutine(Inmunity());

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

    /*
     * Método: heal.
     * Parámetros:
     * @param amount: Cantidad de vida a restaurar.
     * Descripción: Incrementa la vida del jugador.
     */
    private void heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }

    /*
     * Método: Inmunity.
     * Parámetros: Ninguno.
     * Descripción: Activa un periodo de inmunidad temporal tras recibir daño.
     */
    IEnumerator Inmunity()
    {
        playerMoviement.horizontal = 0;
        animator.SetTrigger("getHit");
        isInmune = true;
        yield return new WaitForSeconds(inmunityTime);
        isInmune = false;
    }

    /*
     * Método: UseMana.
     * @param amount: Cantidad de maná a consumir.
     * Descripción: Reduce el maná del jugador.
     */
    public void UseMana(int amount)
    {
        mana = Mathf.Max(0, mana - amount);
    }
}