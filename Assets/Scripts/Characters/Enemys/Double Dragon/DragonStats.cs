using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Clase: DragonStats.
 * Descripción: Gestiona los puntos de vida del dragón y controla su estado.
 */
public class DragonStats : MonoBehaviour
{
    private Enemy enemy; // Referencia al script Enemy

    public int CurrentHealth => enemy.healtPoints; // Vida actual del dragón

    private Animator animator; // Referencia al Animator del dragón
    [Header("Player stats image bar")]
    public Image healtImg; // Imagen de la barra de vida del jugador
    /*
     * Método: Start.
     * Descripción: Inicializa la vida del dragón al valor máximo y obtiene las referencias necesarias.
     */
    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.healtPoints = enemy.MaxHealtPoints; // Inicializa la vida al máximo
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateHealthBar();
    }
    /*
     * Método: TakeDamage.
     * @param damage: Cantidad de daño a aplicar.
     * Descripción: Reduce los puntos de vida del dragón y verifica si está muerto.
     */
    public void TakeDamage(int damage)
    {
        if (enemy == null) return;

        enemy.healtPoints -= damage;

        if (enemy.healtPoints <= 0)
        {
            Die();
        }
    }

    /*
     * Método: Die.
     * Descripción: Maneja la lógica de muerte del dragón.
     */
    private void Die()
    {
        Debug.Log("Dragon has died.");
        animator.SetTrigger("dead");
        StartCoroutine(DestroyAfterDelay(5f)); // Llama a una corrutina para destruir después de 5 segundos
    }

    /*
     * Método: DestroyAfterDelay.
     * @param delay: Tiempo en segundos antes de destruir el GameObject.
     * Descripción: Espera un tiempo antes de destruir el GameObject.
     */
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera la cantidad especificada de segundos
        Destroy(gameObject); // Destruye el GameObject
    }

    private void UpdateHealthBar()
    {
        healtImg.fillAmount = (float)enemy.healtPoints / (float)enemy.MaxHealtPoints;
        Debug.Log($"La vida actual es {enemy.healtPoints} barra de vida: {healtImg.fillAmount}");

        if (enemy.healtPoints > enemy.MaxHealtPoints) enemy.healtPoints = enemy.MaxHealtPoints;
    }
}
