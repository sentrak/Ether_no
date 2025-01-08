using UnityEngine;

public class DropManager : MonoBehaviour
{
    [Header("Drop Settings")]
    [SerializeField] private GameObject panPrefab; // Prefab del pan
    [SerializeField] private GameObject vinoPrefab; // Prefab del vino
    [SerializeField] private float dropChance = 0.5f; // Probabilidad de dropear un objeto (0.0 - 1.0)

    /*
     * Método: DropItem.
     * Parámetros: Ninguno.
     * Descripción: Decide si dropear "pan" o "vino" basado en la probabilidad configurada.
     */
    public void DropItem()
    {
        if (Random.value <= dropChance)
        {
            // Seleccionar aleatoriamente entre pan y vino
            GameObject itemToDrop = Random.value < 0.5f ? panPrefab : vinoPrefab;

            // Instanciar el objeto en la posición del enemigo
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
    }
}
