using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Clase: LevelManager.
 * Descripción: Gestiona la lógica de cambio de escena en función de la interacción del jugador con un trigger.
 */
public class LevelManager : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string targetScene; // Nombre de la escena a cargar

    private BoxCollider2D boxCollider; // Referencia al BoxCollider2D usado como trigger
    private bool triggerState = false; // Estado inicial del trigger

    /*
     * Método: Start.
     * Descripción: Inicializa el estado del trigger y obtiene el BoxCollider2D.
     */
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D is missing from LevelManager GameObject.");
        }
        else
        {
            UpdateTriggerState(); // Configura el estado inicial del trigger
        }
    }

    /*
     * Método: SetTriggerState.
     * @param state: Nuevo estado del trigger.
     * Descripción: Activa o desactiva dinámicamente el trigger.
     */
    public void SetTriggerState(bool state)
    {
        triggerState = state;
        UpdateTriggerState(); // Actualiza el estado del BoxCollider2D inmediatamente
    }

    /*
     * Método: UpdateTriggerState.
     * Descripción: Sincroniza el estado de triggerState con el BoxCollider2D.
     */
    private void UpdateTriggerState()
    {
        if (boxCollider != null)
        {
            boxCollider.isTrigger = triggerState;
            Debug.Log($"Trigger updated: triggerState={triggerState}, isTrigger={boxCollider.isTrigger}");
        }else{
            Debug.LogError("BoxCollider2D is missing from LevelManager GameObject.");
        }
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider que interactúa con el trigger.
     * Descripción: Cambia a la escena especificada si el jugador activa el trigger.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerState && collision.CompareTag("Player"))
        {
            LoadTargetScene();
        }
    }

    /*
     * Método: LoadTargetScene.
     * Descripción: Cambia a la escena especificada en targetScene.
     */
    private void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogWarning("Target scene name is not set.");
        }
    }
}
