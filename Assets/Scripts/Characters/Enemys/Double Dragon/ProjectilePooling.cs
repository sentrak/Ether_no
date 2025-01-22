using System.Collections.Generic;
using UnityEngine;

/*
 * Clase: ProjectilePooling.
 * Descripción: Implementa un sistema de pooling para proyectiles que se disparan hacia un objetivo y se eliminan al
 *              exceder un rango máximo. Diseñado para ser reutilizable por múltiples tipos de proyectiles.
 */
public class ProjectilePooling : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] private int poolSize = 10; // Tamaño del pool de proyectiles
    [SerializeField] private float projectileSpeed = 5f; // Velocidad del proyectil
    [SerializeField] private float maxRange = 20f; // Rango máximo antes de eliminar el proyectil

    [Header("Target Settings")]
    [SerializeField] private Transform targetTransform; // Transform del objetivo hacia el que se disparan los proyectiles

    private Queue<GameObject> projectilePool; // Cola que almacena los proyectiles disponibles

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa el pool de proyectiles.
     */
    private void Start()
    {
        projectilePool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectilePool.Enqueue(projectile);
        }
    }

    /*
     * Método: SpawnProjectile.
     * Parámetros: Ninguno.
     * Descripción: Genera un proyectil desde el pool y lo dirige hacia el objetivo.
     */
    public void SpawnProjectile()
    {
        if (targetTransform == null)
        {
            Debug.LogWarning("Target Transform is not assigned. Cannot spawn projectile.");
            return;
        }

        if (projectilePool.Count > 0)
        {
            GameObject projectile = projectilePool.Dequeue();
            projectile.SetActive(true);

            // Colocar el proyectil en la posición actual del GameObject
            projectile.transform.position = transform.position;

            // Calcular la dirección hacia el objetivo
            Vector3 direction = (targetTransform.position - transform.position).normalized;

            // Configurar el movimiento del proyectil
            projectile.GetComponent<Projectile>().Initialize(direction, projectileSpeed, maxRange, this);
        }
    }

    /*
     * Método: ReturnToPool.
     * @param projectile: Proyectil a regresar al pool.
     * Descripción: Desactiva un proyectil y lo regresa al pool.
     */
    public void ReturnToPool(GameObject projectile)
    {
        projectile.SetActive(false);
        projectilePool.Enqueue(projectile);
    }
}
