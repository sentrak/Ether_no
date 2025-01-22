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
    [SerializeField] private AudioClip portalSound, laugthWitch; // Sonido del portal

    [Header("Settings")]
    [SerializeField] private float witchMoveDistance = 10f; // Distancia que se moverá la bruja a la derecha
    [SerializeField] private float playerWalkSpeed = 2f; // Velocidad con la que el jugador caminará hacia el portal

    private AddictionWitchCircularMovement witchMovement;
    private Animator playerAnimator; // Referencia al Animator del jugador

    private void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
        if (playerAnimator == null)
        {
            Debug.LogError("Animator component is missing on the player GameObject.");
        }
    }

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

    private IEnumerator HandleCollision()
    {
        Debug.Log("Se paró el tiempo");
        // Detener el tiempo del juego
        Time.timeScale = 0f;

        // Reproducir risa de la bruja
        Debug.Log("Se rie la bruja");
        AudioManager.Instance.PlaySound(laugthWitch);
        // Mover la bruja
        Debug.Log("Se mueve la bruja");
        Vector3 targetPosition = witch.transform.position + Vector3.right * witchMoveDistance;
        while (Vector3.Distance(witch.transform.position, targetPosition) > 0.1f)
        {
            witch.transform.position = Vector3.MoveTowards(witch.transform.position, targetPosition, 5 * Time.unscaledDeltaTime);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(1f);
        // Destruir la bruja
        Debug.Log("Se destruye la bruja");
        Destroy(witch);
        // Reproducir sonido del portal
        Debug.Log("Sonó el portal");
        AudioManager.Instance.PlaySound(portalSound);
        // Mover al jugador al portal
        Debug.Log("Jugador se mueve hacia el portal");
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        yield return StartCoroutine(MovePlayerToPortal());
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isRunning", false);
        }
        Debug.Log("Proceso completado");
        Time.timeScale = 1f; // Reanudar el tiempo
        SceneManager.LoadScene("03 Level02");
    }

    private IEnumerator MovePlayerToPortal()
    {
        while (Vector3.Distance(player.transform.position, portal.position) > 0.1f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, portal.position, playerWalkSpeed * Time.unscaledDeltaTime);
            yield return null;
        }
    }
}
