using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip kick,punch,shild,cross;
    public Animator animator; // Referencia al Animator
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();

    }
    /* Método: PerformAttack.
     * Parámetros:
     * @param context: Contexto del evento de ataque del Input System.
     * Descripción: Maneja el ataque del personaje activando la animación correspondiente.
     */
    public void PerformAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AudioManager.Instance.PlaySound(punch);

            // Activar animación de ataque
            animator.SetBool("isAttacking", true);
        }
        else if (context.canceled)
        {
            // Desactivar animación de ataque
            animator.SetBool("isAttacking", false);
        }
    }

    /* Método: UseShield.
     * Parámetros:
     * @param context: Contexto del evento de escudo del Input System.
     * Descripción: Maneja la activación del escudo.
     */
    public void UseShield(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AudioManager.Instance.PlaySound(shild);

            // Activar animación de escudo            
            playerStats.UseMana(5);
            animator.SetBool("isSheltering", true);
        }
        else if (context.canceled)
        {
            // Desactivar animación de escudo
            animator.SetBool("isSheltering", false);
        }
    }

    /* Método: PerformCross.
     * Parámetros:
     * @param context: Contexto del evento de cruz del Input System.
     * Descripción: Maneja el evento de la acción especial "Cross".
     */
    public void PerformCross(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AudioManager.Instance.PlaySound(cross);

            // Activar animación de disparo (Cross)
            animator.SetBool("isShooting", true);
            playerStats.UseMana(10);
        }
        else if (context.canceled)
        {
            // Desactivar animación de disparo
            animator.SetBool("isShooting", false);


        }
    }
}
