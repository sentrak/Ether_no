using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Clase: DragonStats.
 * Descripción: Gestiona los puntos de vida del dragón, actualiza su barra de vida y maneja la lógica de muerte.
 */
public class DragonStats : MonoBehaviour
{
    [Header("Dragon Components")]
    private Enemy enemy; // Referencia al script Enemy que contiene las estadísticas del dragón
    private Animator animator; // Referencia al Animator del dragón

    [Header("UI Settings")]
    [SerializeField] private Image healtImg; // Imagen de la barra de vida del dragón
    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa la vida del dragón al máximo y obtiene las referencias necesarias.
     */
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        enemy.healtPoints = enemy.MaxHealtPoints;
    }

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Actualiza la barra de vida del dragón en cada frame.
     */
    private void Update()
    {
        UpdateHealthBar();
    }

    /*
     * Método: TakeDamage.
     * @param damage: Cantidad de daño a aplicar.
     * Descripción: Reduce los puntos de vida del dragón y verifica si debe morir.
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
     * Parámetros: Ninguno.
     * Descripción: Maneja la lógica de muerte del dragón, activa la animación de muerte y destruye el GameObject después de un retraso.
     */
    private void Die()
    {
        Destroy(healtImg);
        animator.SetTrigger("dead");
        StartCoroutine(DestroyAfterDelay(3.5f));
    }

    /*
     * Método: DestroyAfterDelay.
     * @param delay: Tiempo en segundos antes de destruir el GameObject.
     * Descripción: Destruye el GameObject después de un tiempo específico.
     */
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    /*
     * Método: UpdateHealthBar.
     * Parámetros: Ninguno.
     * Descripción: Actualiza la barra de vida visual del dragón en función de su vida actual.
     */
    private void UpdateHealthBar()
    {
        if (enemy == null) return;

        healtImg.fillAmount = (float)enemy.healtPoints / (float)enemy.MaxHealtPoints;

        if (enemy.healtPoints > enemy.MaxHealtPoints)
        {
            enemy.healtPoints = enemy.MaxHealtPoints;
        }
    }
}
