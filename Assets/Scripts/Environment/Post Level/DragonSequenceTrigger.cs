using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

/*
 * Clase: DragonSequenceTrigger.
 * Descripción: Maneja una secuencia en la que el jugador activa un evento al cruzar un collider, incluyendo
 * la activación y desactivación del dragón, reproducción de una cinemática, y limpieza posterior.
 */
public class DragonSequenceTrigger : MonoBehaviour
{
    [Header("Dragon Settings")]
    [SerializeField] private GameObject dragon; // Referencia al GameObject del dragón
    [SerializeField] private float waitTime = 5f; // Tiempo de espera antes de deshabilitar al dragón

    [Header("Cinematic Settings")]
    [SerializeField] private PlayableDirector playableDirector; // Referencia al PlayableDirector para manejar la cinemática

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider que interactúa con el trigger.
     * Descripción: Activa la cinemática cuando el jugador cruza el trigger y comienza el proceso de desactivación del dragón.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playableDirector != null)
            {
                playableDirector.Play();
            }
            if (dragon != null)
            {
                StartCoroutine(DragonEnableAndDisable());
            }
        }
    }

    /*
     * Método: DragonEnableAndDisable.
     * Parámetros: Ninguno.
     * Descripción: Espera un tiempo específico y luego desactiva o destruye al dragón.
     */
    private IEnumerator DragonEnableAndDisable()
    {
        yield return new WaitForSeconds(waitTime);
        if (dragon != null)
        {
            Destroy(dragon);
        }
    }
}
