using UnityEngine;

public class DropItem : MonoBehaviour
{
    [Header("Item Properties")]
    [SerializeField] private string itemName; // Nombre del objeto (Bread o Wine)
    [SerializeField] private int RecoveryCant; // Cantidad que recupera el jugador
    [Header("Audio Sources")]

    [SerializeField] private AudioClip mana; // Clips de audio
    [SerializeField] private AudioClip bread;// Clips de audio
    /*
     * Método: OnPickup.
     * Parámetros: @param playerStats: Referencia al script PlayerStats del jugador.
     * Descripción: Maneja la lógica de recuperación de vida o maná según el tipo de objeto.
     */
    public void OnPickup(PlayerStats playerStats)
    {
        if (itemName == "Bread")
        {
            playerStats.RecoveryHeal(RecoveryCant);
            AudioManager.Instance.PlaySound(bread);

            Debug.Log($"Has recogido Pan y recuperaste {RecoveryCant} de vida.");
        }
        else if (itemName == "Wine")
        {
            playerStats.RecoveryMana(RecoveryCant);
            AudioManager.Instance.PlaySound(mana);

            Debug.Log($"Has recogido Vino y recuperaste {RecoveryCant} de maná.");
        }

        // Destruir el objeto una vez recogido
        Destroy(gameObject);
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param other: Collider del objeto que entra en contacto.
     * Descripción: Detecta si el jugador recoge el objeto y llama a OnPickup.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                OnPickup(playerStats);
            }
        }
    }
}
