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

    [Header("Trigger Settings")]
    private BoxCollider2D boxCollider; // Referencia al BoxCollider2D usado como trigger
    private bool triggerState = false; // Estado inicial del trigger

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa el estado del trigger y obtiene el BoxCollider2D.
     */
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        UpdateTriggerState(); 
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
     * Parámetros: Ninguno.
     * Descripción: Sincroniza el estado de triggerState con el BoxCollider2D.
     */
    private void UpdateTriggerState()
    {
        if (boxCollider != null)
        {
            boxCollider.isTrigger = triggerState;
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
     * Parámetros: Ninguno.
     * Descripción: Cambia a la escena especificada en targetScene.
     */
    private void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
