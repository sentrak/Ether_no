using UnityEngine;

/*
 * Clase: DropManager.
 * Descripción: Gestiona la lógica para dropear objetos (ítems) cuando un evento ocurre, como la muerte 
 * de un enemigo. Decide qué tipo de objeto dropear (pan o vino) basado en probabilidades configurables.
 */
public class DropManager : MonoBehaviour
{
    [Header("Drop Settings")]
    [SerializeField] private GameObject panPrefab; // Prefab que representa el pan
    [SerializeField] private GameObject vinoPrefab; // Prefab que representa el vino
    [SerializeField] private float dropChance = 0.5f; // Probabilidad de dropear un objeto (0.0 - 1.0)

    [Header("Audio Sources")]
    [SerializeField] private AudioClip dropItem; // Clip de audio reproducido al soltar un item
    /*
     * Método: DropItem.
     * Parámetros: Ninguno.
     * Descripción: Decide si dropear "pan" o "vino" basado en la probabilidad configurada.
     * Si elige dropear, selecciona un prefab y lo genera en una posición específica.
     */
    public void DropItem()
    {
        if (Random.value <= dropChance)
        {
            AudioManager.Instance.PlaySound(dropItem);
            GameObject itemToDrop = Random.value < 0.5f ? panPrefab : vinoPrefab;
            Vector3 position = gameObject.transform.position;
            position.y = -2.3f;
            Instantiate(itemToDrop, position, Quaternion.identity);
        }
    }
}
