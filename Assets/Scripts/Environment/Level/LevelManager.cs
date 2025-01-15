using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string targetScene; // Nombre de la escena a cargar
    [SerializeField] public bool triggerState = true; // Controla si el trigger está activo o no

    private BoxCollider2D boxCollider; // Referencia al BoxCollider2D

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa el BoxCollider2D y configura su estado inicial.
     */
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogError("El GameObject no tiene un BoxCollider2D asignado.", this.gameObject);
            return;
        }
        boxCollider.isTrigger = triggerState;
    }

    /*
     * Método: SetTriggerState.
     * @param state: Nuevo estado del trigger (true para activarlo, false para desactivarlo).
     * Descripción: Permite cambiar el estado del trigger dinámicamente.
     */
    public void SetTriggerState(bool state)
    {
        triggerState = state;
        if (boxCollider != null)
        {
            boxCollider.isTrigger = state;
        }
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider del objeto que entra en contacto.
     * Descripción: Detecta si el jugador entra en el trigger y cambia de escena.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && triggerState)
        {
            if (!string.IsNullOrEmpty(targetScene))
            {
                SceneManager.LoadScene(targetScene); // Cargar la escena especificada
            }
            else
            {
                Debug.LogError("No se ha asignado un nombre de escena en el Inspector.", this.gameObject);
            }
        }
    }
}