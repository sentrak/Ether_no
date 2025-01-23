using UnityEngine;
using System.Collections;

/*
 * Clase: DruggedAttack.
 * Descripción: Gestiona los ataques del enemigo, activando animaciones y pausando temporalmente el movimiento.
 * Coordina la reproducción de audio, animaciones de ataque y detención de acciones durante un intervalo.
 */
public class DruggedAttack : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip attack; // Clip de audio reproducido durante el ataque

    [Header("Enemy Components")]
    private Animator animator; // Referencia al Animator del enemigo

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias necesarias al iniciar el script.
     */
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /*
     * Método: ExecuteAttack.
     * Parámetros: Ninguno.
     * Descripción: Ejecuta el ataque del enemigo, activa el Trigger "attack" en el
     *  Animator, reproduce un sonido y pausa el movimiento durante un tiempo especificado.
     */
    public void ExecuteAttack()
    {
        if (animator != null)
        {
            animator.SetTrigger("attack");
            if (attack != null)
            {
                AudioManager.Instance.PlaySound(attack);
            }
            StartCoroutine(StopMovementForSeconds(2f));
        }
    }

    /*
     * Método: StopMovementForSeconds.
     * @param seconds: Tiempo en segundos durante el cual se detendrá el movimiento.
     * Descripción: Suspende la ejecución del movimiento del enemigo durante el tiempo especificado.
     */
    public IEnumerator StopMovementForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
