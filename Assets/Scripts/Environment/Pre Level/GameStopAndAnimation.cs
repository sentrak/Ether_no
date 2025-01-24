using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/*
 * Clase: GameStopAndAnimation.
 * Descripción: Gestiona la lógica para detener la partida, mover la bruja, reproducir un audio y desplazar al jugador hacia un portal.
 */
public class GameStopAndAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject player; // Referencia al jugador
    private BoxCollider2D boxCollider; // Referencia al collider

    [Header("Dialog Settings")]
    [SerializeField]
    private DialogueScript dialogueScript; // Referencia al script de diálogo

    [Header("Audio Sources")]
    [SerializeField]
    private AudioClip portaSFX; // Clip de audio de portal

    [Header("Cinematic Settings")]
    [SerializeField]
    private PlayableDirector playableDirector; // Referencia al PlayableDirector para manejar la cinemática

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Iniciar el diálogo
            dialogueScript.StartDialogue();
            StartCoroutine(dialogContinuo());
            boxCollider.enabled = false;
        }
    }

    private IEnumerator dialogContinuo()
    {
        while (!dialogueScript.isFinished)
        {
            yield return null; // Esperar un frame
        }
        playableDirector.Play();
        AudioManager.Instance.PlaySound(portaSFX);
        playableDirector.stopped += OnTimelineStopped;
        AudioManager.Instance.PlaySound(portaSFX);

    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        SceneManager.LoadScene("03 Level02");
    }
}
