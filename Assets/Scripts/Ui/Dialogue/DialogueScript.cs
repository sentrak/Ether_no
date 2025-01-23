using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private GameObject dialogueContainer; // Contenedor del diálogo

    [SerializeField] private Image characterImage; // Imagen del personaje
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Sprite[] characterSprites; // Sprites de los personajes (Player y Bruja)
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private int[] characterIndices;

    private int currentDialogueIndex = 0;

    void Start()
    {
        dialogueContainer.SetActive(false);
        nextButton.onClick.AddListener(ShowNextDialogue);
    }

    public void StartDialogue()
    {

        Debug.Log("Iniciando diálogo.... en dialoguescript");
        currentDialogueIndex = 0; // Reinicia el índice del diálogo
        dialogueContainer.SetActive(true); // Activa el cuadro de diálogo
        Debug.Log("Container activado: " + dialogueContainer.activeSelf);
        UpdateDialogue(); // Muestra la primera línea
    }

    private void ShowNextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex >= dialogueLines.Length)
        {
            EndDialogue(); // Termina el diálogo si no hay más líneas
        }
        else
        {
            UpdateDialogue(); // Muestra la siguiente línea
        }
    }

    private void UpdateDialogue()
    {
        Debug.Log("Actualizando diálogo: " + dialogueLines[currentDialogueIndex]);
        dialogueText.text = dialogueLines[currentDialogueIndex]; 
        characterImage.sprite = characterSprites[characterIndices[currentDialogueIndex]]; // Cambia la imagen del personaje
    }

    private void EndDialogue()
    {
        dialogueContainer.SetActive(false); // Oculta el cuadro de diálogo
        Debug.Log("Diálogo terminado");
    }
}
