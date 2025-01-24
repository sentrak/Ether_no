using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

/*
 * Clase: FriarInteraction.
 * Descripción: Gestiona la interacción del jugador con el fraile (Friar), incluyendo mostrar un sprite de interacción,
 * dropear ítems, mover al fraile y activar un teleport. Desactiva temporalmente el movimiento del jugador durante la interacción.
 */
public class FriarInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField]
    private GameObject interactionSprite; // Sprite que indica la interacción

    [SerializeField]
    private GameObject tp; // Teleport al mundo 2

    [SerializeField]
    private GameObject prefab1; // Primer prefab que se dropeará

    [SerializeField]
    private GameObject prefab2; // Segundo prefab que se dropeará

    [SerializeField]
    private Transform dropPosition; // Posición donde se dropearán los prefabs

    [SerializeField]
    private float moveDistance = 10f; // Distancia que el fraile se moverá

    [SerializeField]
    private float moveSpeed = 7f; // Velocidad del movimiento del fraile

    [Header("Player Settings")]
    [SerializeField]
    private PlayerMoviement playerMoviement; // Referencia al script PlayerMoviement del jugador

    [Header("Audio Sources")]
    [SerializeField]
    private AudioClip dropItem; // Clip de audio reproducido al soltar un ítem

    [SerializeField]
    private AudioClip walk; // Clip de audio reproducido al caminar

    [Header("Collider Components")]
    private CapsuleCollider2D capsuleCollider2D; // Referencia al CapsuleCollider2D
    private BoxCollider2D boxCollider2D; // Referencia al BoxCollider2D

    [Header("Dialog Settings")]
    [SerializeField]
    private DialogueScript dialogueScript; // Referencia al script de diálogo

    [Header("Internal References")]
    private bool isPlayerNearby = false; // Indica si el jugador está dentro del rango de interacción
    private bool isInteracting = false; // Indica si ya se está realizando una interacción
    private Animator animator; // Referencia al Animator del fraile
    private PrefabSpawner prefabSpawner; // Referencia al PrefabSpawner para generar prefabs
    private LevelManager levelManager; // Referencia al LevelManager asociado al teleport

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias necesarias, desactiva el sprite de
     * interacción y emite una advertencia si falta la referencia al PlayerMoviement.
     */
    private void Start()
    {
        levelManager = tp.GetComponent<LevelManager>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        prefabSpawner = GetComponent<PrefabSpawner>();
        interactionSprite?.SetActive(false);
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param other: Collider del objeto que entra en contacto con el trigger.
     * Descripción: Activa el sprite de interacción si el jugador entra en el rango del fraile.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactionSprite?.SetActive(true);
        }
    }

    /*
     * Método: OnTriggerExit2D.
     * @param other: Collider del objeto que sale del rango del trigger.
     * Descripción: Desactiva el sprite de interacción si el jugador sale del rango del fraile.
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactionSprite?.SetActive(false);
        }
    }

    /*
     * Método: Interact.
     * @param context: Contexto del Input System que captura la interacción del jugador.
     * Descripción: Maneja la interacción, desactiva el movimiento del jugador,
     * dropea ítems y activa el proceso de movimiento y destrucción del fraile.
     */
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && isPlayerNearby && !isInteracting)
        {
            dialogueScript.StartDialogue();
            StartCoroutine(dialogContinuo());
        }
    }

    /*
     * Método: DropItems.
     * Parámetros: Ninguno.
     * Descripción: Dropea dos prefabs en posiciones cercanas al fraile y reproduce un sonido.
     */
    private void DropItems()
    {
        if (prefab1 == null || prefab2 == null || dropPosition == null)
            return;

        AudioManager.Instance.PlaySound(dropItem);
        Instantiate(
            prefab1,
            dropPosition.position + new Vector3(3f, -2.3f, 0f),
            Quaternion.identity
        );
        Instantiate(
            prefab2,
            dropPosition.position + new Vector3(2f, -1.5f, 0f),
            Quaternion.identity
        );
    }

    /*
     * Método: MoveAndDestroy.
     * Parámetros: Ninguno.
     * Descripción: Mueve al fraile una distancia específica, genera más prefabs, activa el teleport
     *              y destruye al fraile después de completar la interacción.
     */
    private System.Collections.IEnumerator MoveAndDestroy()
    {
        AudioManager.Instance.PlaySound(walk);
        Vector3 targetPosition = transform.position + Vector3.right * moveDistance;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.unscaledDeltaTime
            );
            yield return null;
        }

        animator?.SetBool("run", false);
        SpawnPrefabs();
        levelManager.SetTriggerState(true);

        playerMoviement.IsUsingSkill = false;
        Destroy(gameObject);
    }

    /*
     * Método: SpawnPrefabs.
     * Parámetros: Ninguno.
     * Descripción: Genera tres prefabs en posiciones específicas después de completar el movimiento del fraile.
     */
    private void SpawnPrefabs()
    {
        prefabSpawner.SpawnPrefab(-10.1f, -0.1000001f);
        prefabSpawner.SpawnPrefab(-4.8f, -0.1000001f);
        prefabSpawner.SpawnPrefab(4f, -0.1000001f);
    }

    private IEnumerator dialogContinuo()
    {
        while (!dialogueScript.isFinished)
        {
            yield return null; // Esperar un frame
        }
        isInteracting = true;
        Destroy(boxCollider2D);
        interactionSprite?.SetActive(false);
        playerMoviement.IsUsingSkill = true;
        capsuleCollider2D.isTrigger = true;
        DropItems();
        animator?.SetBool("run", true);
        StartCoroutine(MoveAndDestroy());
    }
}
