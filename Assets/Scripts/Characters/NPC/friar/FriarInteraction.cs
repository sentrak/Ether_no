using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FriarInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private GameObject interactionSprite; // Referencia al GameObject hijo con el sprite de la "A"
    [SerializeField] private GameObject tp; // Referencia al tp al mundo 2
    [SerializeField] private GameObject prefab1; // Primer prefab a dropear
    [SerializeField] private GameObject prefab2; // Segundo prefab a dropear
    [SerializeField] private Transform dropPosition; // Posición donde se dropearán los prefabs
    [SerializeField] private float moveDistance = 5f; // Distancia que el frailer se moverá
    [SerializeField] private float moveSpeed = 2f; // Velocidad del movimiento del frailer
    private bool isPlayerNearby = false; // Indica si el jugador está dentro del rango de interacción
    private bool isInteracting = false; // Indica si la interacción está en curso
    private Animator animator; // Referencia al Animator del frailer
    private PrefabSpawner prefabSpawner; // Referencia al PrefabSpawner del frailer
    private LevelManager levelManager; // Referencia al script LevelManager del GameObject tp

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Asegura que el sprite de interacción esté desactivado al iniciar.
     */
    private void Start()
    {
        levelManager = tp.GetComponent<LevelManager>();
        animator = GetComponent<Animator>();
        prefabSpawner = GetComponent<PrefabSpawner>();
        if (interactionSprite != null)
        {
            interactionSprite.SetActive(false);
        }
        else
        {
            Debug.LogError("No se asignó el sprite de interacción en el Inspector.");
        }
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param other: Collider del objeto que entra en contacto.
     * Descripción: Muestra el sprite de interacción si el jugador entra en el rango.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (interactionSprite != null)
            {
                interactionSprite.SetActive(true);
            }
        }
    }

    /*
     * Método: OnTriggerExit2D.
     * @param other: Collider del objeto que sale del contacto.
     * Descripción: Oculta el sprite de interacción si el jugador sale del rango.
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactionSprite != null)
            {
                interactionSprite.SetActive(false);
            }
        }
    }

    /*
     * Método: Interact.
     * @param context: Contexto del Input System para detectar interacción.
     * Descripción: Maneja la interacción cuando el jugador está cerca y presiona el botón.
     */
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && isPlayerNearby && !isInteracting)
        {
            isInteracting = true;
            if (interactionSprite != null)
            {
                interactionSprite.SetActive(false); // Ocultar el sprite al interactuar
            }
            Debug.Log("Se ha interactuado con el friar.");

            // Pausar el juego
            Time.timeScale = 0f;

            // Dropear los prefabs
            DropItems();

            // Activar la animación de caminar y moverse
            StartCoroutine(MoveAndDestroy());
        }
    }

    /*
     * Método: DropItems.
     * Parámetros: Ninguno.
     * Descripción: Dropea dos prefabs en la posición especificada.
     */
    private void DropItems()
    {
        if (prefab1 != null && prefab2 != null && dropPosition != null)
        {
            Vector3 position1 = new Vector3(dropPosition.position.x + 3f, -2.3f, dropPosition.position.y);
            Vector3 position2 = new Vector3(dropPosition.position.x + 2f, -2.3f, dropPosition.position.y);
            Instantiate(prefab1, position1, Quaternion.identity);
            Instantiate(prefab2, position2, Quaternion.identity);
            Debug.Log("Prefabs dropeados.");
        }
        else
        {
            Debug.LogError("No se asignaron los prefabs o la posición de dropeo en el Inspector.");
        }
    }

    /*
     * Método: MoveAndDestroy.
     * Parámetros: Ninguno.
     * Descripción: Mueve al frailer una distancia específica, lo destruye y despausa el juego.
     */
    private System.Collections.IEnumerator MoveAndDestroy()
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", true); // Activar la animación de caminar
        }

        Vector3 targetPosition = transform.position + Vector3.right * moveDistance; // Calcular la posición objetivo

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.unscaledDeltaTime);
            yield return null;
        }

        if (animator != null)
        {
            animator.SetBool("isWalking", false); // Detener la animación de caminar
        }
        prefabSpawner.SpawnPrefab(-10.1f, -0.1000001f); 
        prefabSpawner.SpawnPrefab(-4.8f, -0.1000001f); 
        prefabSpawner.SpawnPrefab(4f, -0.1000001f);
        Destroy(gameObject); // Destruir el frailer
        Time.timeScale = 1f; // Despausar el juego
        Debug.Log("El frailer ha terminado su interacción y se ha destruido.");
         levelManager.triggerState = true;
    }
    
}
