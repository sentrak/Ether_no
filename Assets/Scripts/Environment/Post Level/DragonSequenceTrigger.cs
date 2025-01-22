using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

/*
 * Clase: DragonSequenceTrigger.
 * Descripción: Maneja la secuencia cuando el jugador cruza un collider, incluyendo mover al jugador,
 * habilitar al dragón, ejecutar animaciones y deshabilitarlo al finalizar.
 */
public class DragonSequenceTrigger : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private GameObject dragon; // Referencia al GameObject del dragón
    [SerializeField] private float WaitTime; // Referencia al GameObject del dragón
    public PlayableDirector playableDirector; //
    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playableDirector != null)
        {
            playableDirector.Play();
        }
    }
    private IEnumerable DragonEnableAndDesable()
    {
        yield return new WaitForSeconds(WaitTime);
        Destroy(dragon);
        
    }
}
