using UnityEngine;

/*
 * Clase: DropItem.
 * Descripción: Gestiona la interacción de los objetos recolectables en el juego. Los objetos 
 * pueden restaurar vida o maná del jugador según su tipo, y se destruyen una vez recogidos.
 */
public class DropItem : MonoBehaviour
{
    [Header("Item Properties")]
    [SerializeField] private string itemName; // Nombre del objeto (por ejemplo, "Bread" o "Wine")
    [SerializeField] private int RecoveryCant; // Cantidad de vida o maná que recupera el jugador

    [Header("Audio Sources")]
    [SerializeField] private AudioClip potion; // Clip de audio reproducido al recoger un objeto 

    /*
     * Método: OnPickup.
     * @param playerStats: Referencia al script PlayerStats del jugador.
     * Descripción: Maneja la lógica de recuperación de vida o maná según el tipo de objeto y reproduce el audio correspondiente.
     */
    public void OnPickup(PlayerStats playerStats)
    {
        if (itemName == "Bread")
        {
            playerStats.RecoveryHeal(RecoveryCant);
            AudioManager.Instance.PlaySound(potion);
        }
        else if (itemName == "Wine")
        {
            playerStats.RecoveryMana(RecoveryCant);
            AudioManager.Instance.PlaySound(potion);
        }
        Destroy(gameObject);
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param other: Collider del objeto que entra en contacto.
     * Descripción: Detecta si el jugador recoge el objeto y llama al método OnPickup.
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
