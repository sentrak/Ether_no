using UnityEngine;

/*
 * Clase: DragonStats.
 * Descripción: Gestiona los puntos de vida del dragón y controla su estado.
 */
public class DragonStats : MonoBehaviour
{
    private Enemy enemy; // Referencia al script Enemy

    public int CurrentHealth => enemy.healtPoints; // Vida actual del dragón

    private Animator animator; // Referencia al Animator del dragón

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

    /*
     * Método: TakeDamage.
     * @param damage: Cantidad de daño a aplicar.
     * Descripción: Reduce los puntos de vida del dragón y verifica si está muerto.
     */
    public void TakeDamage(int damage)
    {
        if (enemy == null) return;

        enemy.healtPoints -= damage;
        Debug.Log($"Dragon took {damage} damage. Current health: {enemy.healtPoints}");

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
        // Agregar lógica adicional para la muerte del dragón, como deshabilitar componentes o generar recompensas.
    }
}
