using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueScript dialogueScript; // Referencia al script de diálogo
    //private bool dialogueStarted = false; // Control para evitar que se inicie varias veces

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.CompareTag("Player"))
      {
        Debug.Log("Colisión detectada con el jugador. Iniciando diálogo.");
        dialogueScript.StartDialogue(); // Llamar al método de diálogo
      }
}
}
