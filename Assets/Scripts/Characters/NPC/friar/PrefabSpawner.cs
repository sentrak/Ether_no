using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject prefabToSpawn; // Prefab que será spawneado

    /*
     * Método: SpawnPrefab.
     * @param positionX: Coordenada X donde se generará el prefab.
     * @param positionY: Coordenada Y donde se generará el prefab.
     * Descripción: Genera el prefab asignado en el inspector en las coordenadas especificadas.
     */
    public void SpawnPrefab(float positionX, float positionY)
    {
        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab no asignado al PrefabSpawner.");
            return;
        }

        // Crear la posición del prefab
        Vector3 spawnPosition = new Vector3(positionX, positionY, 0f);

        // Instanciar el prefab en la posición indicada
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        Debug.Log($"Prefab spawneado en la posición: {spawnPosition}");
    }
}
