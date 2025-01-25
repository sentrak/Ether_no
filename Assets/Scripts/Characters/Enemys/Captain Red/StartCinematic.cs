using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class StartCinematic : MonoBehaviour
{
    [Header("Dialog Settings")]
    [SerializeField]
    private DialogueScript dialogueScript; // Referencia al script de diálogo

    [Header("Audio Sources")]
    [SerializeField]
    private AudioClip laughtSFX; // Clip de audio de risa

    [Header("Cinematic Settings")]
    [SerializeField]
    private PlayableDirector playableDirector; // Referencia al PlayableDirector para manejar la cinemática
    void Awake()
    {
        dialogueScript.StartDialogue();
        StartCoroutine(dialogContinuo());
    }

    private IEnumerator dialogContinuo()
    {
        while (!dialogueScript.isFinished)
        {
            yield return null; // Esperar un frame
        }
        playableDirector.Play();
        AudioManager.Instance.PlaySound(laughtSFX);
    }


}
