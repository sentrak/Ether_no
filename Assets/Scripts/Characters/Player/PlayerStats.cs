using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Clase: PlayerStats.
 * Descripción: Gestiona las estadísticas del jugador, incluyendo vida y maná, y actualiza las barras de estado visuales.
 * También maneja eventos importantes como recuperación, reducción de vida/maná y la muerte del jugador.
 */
public class PlayerStats : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip death; // Clip de audio que se reproduce al morir el jugador

    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 100; // Vida máxima del jugador
    [SerializeField] private int maxMana = 50; // Maná máximo del jugador

    public float health; // Vida actual del jugador
    public float mana; // Maná actual del jugador

    [Header("Player Stats Image Bars")]
    [SerializeField] private Image healtImg; // Imagen de la barra de vida del jugador
    [SerializeField] private Image ManaImg; // Imagen de la barra de maná del jugador

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa los valores de vida y maná del jugador al máximo.
     */
    private void Start()
    {
        health = maxHealth;
        mana = maxMana;
    }

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Actualiza las barras de vida y maná del jugador y maneja el evento de muerte si la vida llega a 0.
     */
    private void Update()
    {
        UpdateHealthBar();
        UpdateManaBar();

        if (health <= 0)
        {
            HandleDeath();
        }
    }

    /*
     * Método: UpdateHealthBar.
     * Parámetros: Ninguno.
     * Descripción: Actualiza la barra de vida visual del jugador según su vida actual.
     */
    private void UpdateHealthBar()
    {
        healtImg.fillAmount = health / maxHealth;
        if (health > maxHealth) health = maxHealth;
    }

    /*
     * Método: UpdateManaBar.
     * Parámetros: Ninguno.
     * Descripción: Actualiza la barra de maná visual del jugador según su maná actual.
     */
    private void UpdateManaBar()
    {
        ManaImg.fillAmount = mana / maxMana;
        if (mana > maxMana) mana = maxMana;
    }

    /*
     * Método: HandleDeath.
     * Parámetros: Ninguno.
     * Descripción: Maneja el evento de muerte del jugador, reproduce un sonido de muerte y carga la escena de Game Over.
     */
    private void HandleDeath()
    {
        AudioManager.Instance.PlaySound(death);
        health = 0;
        SceneManager.LoadScene("05 game over");
    }

    /*
     * Método: RecoveryHeal.
     * @param amount: Cantidad de vida a recuperar.
     * Descripción: Recupera vida del jugador hasta el máximo permitido.
     */
    public void RecoveryHeal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }

    /*
     * Método: RecoveryMana.
     * @param amount: Cantidad de maná a recuperar.
     * Descripción: Recupera maná del jugador hasta el máximo permitido.
     */
    public void RecoveryMana(int amount)
    {
        mana = Mathf.Min(mana + amount, maxMana);
    }

    /*
     * Método: ReduceHealth.
     * @param amount: Cantidad de vida a reducir.
     * Descripción: Reduce la vida del jugador, asegurando que no sea menor que 0.
     */
    public void ReduceHealth(int amount)
    {
        health = Mathf.Max(0, health - amount);
    }

    /*
     * Método: ReduceMana.
     * @param amount: Cantidad de maná a reducir.
     * Descripción: Reduce el maná del jugador, asegurando que no sea menor que 0.
     */
    public void ReduceMana(int amount)
    {
        mana = Mathf.Max(0, mana - amount);
    }

    /*
     * Método: Die.
     * Parámetros: Ninguno.
     * Descripción: Maneja la lógica de muerte del jugador, incluyendo la desactivación de movimiento y la carga de la escena de Game Over.
     */
    public void Die()
    {
        SceneManager.LoadScene("05 game over");
    }
}
