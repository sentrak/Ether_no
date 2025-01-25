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

    [SerializeField]
    private bool isCapitanRed = false;
    void Start()
    {

        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           Time.timeScale = 0f;
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
        playableDirector.stopped += OnTimelineStopped;


    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if (!isCapitanRed)
        {
            AudioManager.Instance.PlaySound(portaSFX);
            SceneManager.LoadScene("03 Level02");
        }
        Time.timeScale = 1f;
    }
}
