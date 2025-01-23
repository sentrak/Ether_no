using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Clase: GameStopAndAnimation.
 * Descripción: Gestiona la lógica para detener la partida, mover la bruja, reproducir un audio y desplazar al jugador hacia un portal.
 */
public class GameStopAndAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player; // Referencia al jugador
    [SerializeField] private GameObject witch; // Referencia a la bruja
    [SerializeField] private Transform portal; // Referencia al portal

    [Header("Audio Settings")]
    [SerializeField] private AudioClip portalSound; // Sonido del portal
    [SerializeField] private AudioClip laugthWitch; // Sonido de risa de la bruja

    [Header("Settings")]
    [SerializeField] private float witchMoveDistance = 10f; // Distancia que se moverá la bruja a la derecha
    [SerializeField] private float playerWalkSpeed = 2f; // Velocidad con la que el jugador caminará hacia el portal

    private AddictionWitchCircularMovement witchMovement; // Referencia al script de movimiento circular de la bruja
    private Animator playerAnimator; // Referencia al Animator del jugador

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias necesarias, incluyendo el Animator del jugador.
     */
    private void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider del objeto que entra en el trigger.
     * Descripción: Detiene el movimiento del tiempo, activa la secuencia y maneja la interacción con el jugador.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            witchMovement = witch.GetComponent<AddictionWitchCircularMovement>();
            if (witchMovement != null)
            {
                witchMovement.isAnimating = true;
            }
            StartCoroutine(HandleCollision());
        }
    }

    /*
     * Método: HandleCollision.
     * Parámetros: Ninguno.
     * Descripción: Detiene el tiempo del juego, mueve la bruja, destruye la bruja, reproduce sonidos y mueve al jugador hacia el portal.
     */
    private IEnumerator HandleCollision()
    {
        Time.timeScale = 0f; 
        AudioManager.Instance.PlaySound(laugthWitch); 
        Vector3 targetPosition = witch.transform.position + Vector3.right * witchMoveDistance;
        while (Vector3.Distance(witch.transform.position, targetPosition) > 0.1f)
        {
            witch.transform.position = Vector3.MoveTowards(witch.transform.position, targetPosition, 5 * Time.unscaledDeltaTime);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(1f);
        Destroy(witch);
        AudioManager.Instance.PlaySound(portalSound); 

        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        yield return StartCoroutine(MovePlayerToPortal());
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isRunning", false);
        }
        Time.timeScale = 1f; 
        SceneManager.LoadScene("03 Level02"); 
    }

    /*
     * Método: MovePlayerToPortal.
     * Parámetros: Ninguno.
     * Descripción: Mueve al jugador suavemente hacia el portal.
     */
    private IEnumerator MovePlayerToPortal()
    {
        while (Vector3.Distance(player.transform.position, portal.position) > 0.1f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, portal.position, playerWalkSpeed * Time.unscaledDeltaTime);
            yield return null;
        }
    }
}
