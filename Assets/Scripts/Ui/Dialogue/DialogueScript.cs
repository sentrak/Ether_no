using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private GameObject dialogueContainer; // Contenedor del diálogo
    [SerializeField] private Image characterImage; // Imagen del personaje
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Sprite[] characterSprites; // Sprites de los personajes (Player y Bruja)
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private int[] characterIndices;
    public bool isFinished { get; private set; } = false; // Indica si el diálogo finalizó
    private int currentDialogueIndex = 0;

    private PlayerInput playerInput; // Referencia al Input System del jugador

    void Start()
    {
        dialogueContainer.SetActive(false);
        nextButton.onClick.AddListener(ShowNextDialogue);

        playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
        {
            playerInput.actions["Interact"].performed += OnInteractPerformed;
        }
        else
        {
            Debug.LogError("No se encontró el PlayerInput en la escena.");
        }
    }

    private void OnDestroy()
    {
        if (playerInput != null)
        {
            playerInput.actions["Interact"].performed -= OnInteractPerformed;
        }
    }

    public void StartDialogue()
    {
        isFinished = false;
        currentDialogueIndex = 0; // Reinicia el índice del diálogo
        if (dialogueContainer != null)
        {
            dialogueContainer.SetActive(true); // Activa el cuadro de diálogo
        }
        Time.timeScale = 0f; // Pausar el juego
        UpdateDialogue(); // Muestra la primera línea
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        // Verificar si el cuadro de diálogo sigue existiendo y está activo
        if (dialogueContainer != null && dialogueContainer.activeSelf)
        {
            ShowNextDialogue();
        }
        else
        {
            Debug.LogWarning("El diálogo no está activo o ha sido destruido.");
        }
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
        if (dialogueText != null && characterImage != null)
        {
            dialogueText.text = dialogueLines[currentDialogueIndex];
            characterImage.sprite = characterSprites[characterIndices[currentDialogueIndex]]; // Cambia la imagen del personaje
        }
        else
        {
            Debug.LogWarning("Faltan referencias para actualizar el diálogo.");
        }
    }

    private void EndDialogue()
    {
        if (dialogueContainer != null)
        {
            dialogueContainer.SetActive(false); // Oculta el cuadro de diálogo
        }
        Time.timeScale = 1f; // Reanudar el juego
        isFinished = true;
    }
}
