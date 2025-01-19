using UnityEngine;
using System.Collections;

/*
 * Clase: DruggedAttack.
 * Descripción: Gestiona los ataques del enemigo, activando animaciones y controlando pausas temporales durante el movimiento.
 * Esta clase se utiliza para coordinar la ejecución de un ataque y la suspensión del movimiento asociado.
 */
public class DruggedAttack : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip attack; // Clip de audio reproducido al atacar 

    private Animator animator; // Referencia al Animator 

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Obtiene el componente Animator del GameObject al iniciar el script.
     */
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /*
     * Método: ExecuteAttack.
     * Parámetros: Ninguno.
     * Descripción: Ejecuta un ataque activando el Trigger "attack" en el Animator y pausa el movimiento por un tiempo especificado.
     */
    public void ExecuteAttack()
    {
        if (animator != null)
        {
            animator.SetTrigger("attack");
            AudioManager.Instance.PlaySound(attack);
            StartCoroutine(StopMovementForSeconds(2f));
        }
    }

    /*
     * Método: StopMovementForSeconds.
     * @param seconds: Tiempo en segundos durante el cual el movimiento estará detenido.
     * Descripción: Pausa la ejecución durante el tiempo especificado.
     */
    public IEnumerator StopMovementForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
