using UnityEngine;

/*
 * Clase: Projectile.
 * Descripción: Gestiona el movimiento y la eliminación de un proyectil basado en el rango máximo.
 */
public class Projectile : MonoBehaviour
{
    private Vector3 direction; // Dirección del proyectil
    private float speed; // Velocidad del proyectil
    private float maxRange; // Rango máximo del proyectil
    private Vector3 startPosition; // Posición inicial del proyectil
    private ProjectilePooling poolManager; // Referencia al pool manager
    /*
     * Método: Initialize.
     * @param direction: Dirección hacia donde se moverá el proyectil.
     * @param speed: Velocidad del proyectil.
     * @param maxRange: Rango máximo antes de eliminar el proyectil.
     * @param poolManager: Referencia al pool manager.
     * Descripción: Configura los parámetros iniciales del proyectil.
     */
    public void Initialize(Vector3 direction, float speed, float maxRange, ProjectilePooling poolManager)
    {
        this.speed = speed;
        this.maxRange = maxRange;
        this.startPosition = transform.position;
        this.poolManager = poolManager;
        this.direction = direction;
        RotateTowardsDirection(direction);
    }

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Gestiona el movimiento del proyectil y verifica si excede el rango máximo.
     */
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (Vector3.Distance(startPosition, transform.position) >= maxRange)
        {
            poolManager.ReturnToPool(gameObject);
        }
    }

    /*
     * Método: RotateTowardsDirection.
     * @param direction: Dirección hacia donde apunta el proyectil.
     * Descripción: Ajusta la rotación del proyectil para que apunte hacia la dirección especificada.
     */
    private void RotateTowardsDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider del objeto con el que colisiona el proyectil.
     * Descripción: Maneja la colisión con el objeto que tenga el tag "Player".
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerHitArea"))
        {
            Debug.Log("Projectile hit the Player!");
            poolManager.ReturnToPool(gameObject);
        }
    }
}
