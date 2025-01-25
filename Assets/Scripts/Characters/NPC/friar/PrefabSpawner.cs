using UnityEngine;

/*
 * Clase: PrefabSpawner.
 * Descripción: Gestiona la generación de prefabs en posiciones específicas del mundo.
 * Proporciona un método público para generar un prefab en coordenadas X e Y especificadas.
 */
public class PrefabSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject prefabToSpawn; // Prefab que será generado en la posición indicada

    /*
     * Método: SpawnPrefab.
     * @param positionX: Coordenada X donde se generará el prefab.
     * @param positionY: Coordenada Y donde se generará el prefab.
     * Descripción: Genera el prefab asignado en el inspector en las coordenadas especificadas.
     */
    public void SpawnPrefab(float positionX, float positionY)
    {
        Vector3 spawnPosition = new Vector3(positionX, positionY, 0f);
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
