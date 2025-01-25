using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FriarInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private GameObject interactionSprite;
    [SerializeField] private GameObject tp;
    [SerializeField] private GameObject prefab1;
    [SerializeField] private GameObject prefab2;
    [SerializeField] private Transform dropPosition;
    [SerializeField] private float moveDistance = 10f;
    [SerializeField] private float moveSpeed = 7f;

    [Header("Player Settings")]
    [SerializeField] private PlayerMoviement playerMoviement;

    [Header("Audio Sources")]
    [SerializeField] private AudioClip dropItem;
    [SerializeField] private AudioClip walk;

    [Header("Dialog Settings")]
    [SerializeField] private DialogueScript dialogueScript;

    private bool isPlayerNearby = false;
    private bool isInteracting = false;
    private Animator animator;
    private PrefabSpawner prefabSpawner;
    private LevelManager levelManager;
    private Collider2D[] colliders; // Almacena los colliders del objeto

    private void Start()
    {
        levelManager = tp.GetComponent<LevelManager>();
        animator = GetComponent<Animator>();
        prefabSpawner = GetComponent<PrefabSpawner>();
        colliders = GetComponents<Collider2D>(); // Obtiene todos los colliders del objeto
        interactionSprite?.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isInteracting) return; // Bloquea la interacción si ya está en progreso

        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactionSprite?.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isInteracting) return; // Bloquea el cambio si ya está en progreso

        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactionSprite?.SetActive(false);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && isPlayerNearby && !isInteracting)
        {
            SetCollidersTrigger(true); // Configura ambos colliders como triggers
            isInteracting = true; // Bloquear nuevas interacciones
            interactionSprite?.SetActive(false);

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            dialogueScript.StartDialogue();
            StartCoroutine(DialogContinuo());
        }
    }

    private void DropItems()
    {
        if (prefab1 == null || prefab2 == null || dropPosition == null)
            return;

        AudioManager.Instance.PlaySound(dropItem);
        Instantiate(prefab1, dropPosition.position + new Vector3(3f, -2.3f, 0f), Quaternion.identity);
        Instantiate(prefab2, dropPosition.position + new Vector3(2f, -1.5f, 0f), Quaternion.identity);
    }

    private IEnumerator MoveAndDestroy()
    {
        AudioManager.Instance.PlaySound(walk);
        animator?.SetBool("run", true);
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
        this.enabled = false;
        SpawnPrefabs();
        levelManager.SetTriggerState(true);
        playerMoviement.IsUsingSkill = false;
        
    }

    private void SpawnPrefabs()
    {
        prefabSpawner.SpawnPrefab(-10.1f, -0.1000001f);
        prefabSpawner.SpawnPrefab(-4.8f, -0.1000001f);
        prefabSpawner.SpawnPrefab(4f, -0.1000001f);
    }

    private IEnumerator DialogContinuo()
    {
        while (!dialogueScript.isFinished)
        {
            yield return null; // Esperar un frame hasta que el diálogo termine
        }

        DropItems();
        animator?.SetBool("run", true);
        StartCoroutine(MoveAndDestroy());
    }

    private void SetCollidersTrigger(bool isTrigger)
    {
        foreach (var collider in colliders)
        {
            collider.isTrigger = isTrigger;
        }
    }
}
