// PlayerSkills.cs
using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Clase: PlayerSkills.
 * Descripción: Gestiona las habilidades del jugador, como ataques, uso del escudo y habilidades especiales.
 *              Controla el consumo de maná, activa animaciones y sincroniza el uso de habilidades con el movimiento.
 */
public class PlayerSkills : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip punch; // Sonido para el ataque
    [SerializeField] private AudioClip shield; // Sonido para el uso del escudo
    [SerializeField] private AudioClip cross; // Sonido para la habilidad especial "Cross"

    [Header("Player Components")]
    [SerializeField] private Animator animator; // Referencia al Animator para activar las animaciones
    private PlayerStats playerStats; // Referencia al script PlayerStats que gestiona la vida y el maná del jugador
    private PlayerMoviement playerMoviement; // Referencia al script PlayerMoviement para sincronizar habilidades con el movimiento

    [Header("Mana Settings")]
    [SerializeField] private int crossManaCost = 10; // Costo de maná para usar la habilidad "Cross"
    [SerializeField] private int shieldManaCost = 5; // Costo de maná para usar el escudo

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias necesarias a PlayerStats y PlayerMoviement.
     */
    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        playerMoviement = GetComponent<PlayerMoviement>();
    }

    /*
     * Método: PerformAttack.
     * @param context: Contexto del Input System para la acción de ataque.
     * Descripción: Gestiona el ataque del jugador activando animaciones y sonidos.
     */
    public void PerformAttack(InputAction.CallbackContext context)
    {
        HandleSkill(context, punch, "isAttacking", 0);
    }

    /*
     * Método: UseShield.
     * @param context: Contexto del Input System para la acción de escudo.
     * Descripción: Maneja el uso del escudo, consumiendo maná y activando animaciones y sonidos.
     */
    public void UseShield(InputAction.CallbackContext context)
    {
        HandleSkill(context, shield, "isSheltering", shieldManaCost);
    }

    /*
     * Método: PerformCross.
     * @param context: Contexto del Input System para la habilidad "Cross".
     * Descripción: Maneja la ejecución de la habilidad especial "Cross", consumiendo maná y activando animaciones y sonidos.
     */
    public void PerformCross(InputAction.CallbackContext context)
    {
        HandleSkill(context, cross, "isShooting", crossManaCost);
    }

    /*
     * Método: HandleSkill.
     * @param context: Contexto del Input System para la acción de la habilidad.
     * @param sound: Clip de audio que se reproduce al usar la habilidad.
     * @param animationTrigger: Nombre del trigger de animación correspondiente.
     * @param manaCost: Costo de maná necesario para ejecutar la habilidad.
     * Descripción: Gestiona el inicio y la finalización de una habilidad, 
     * sincronizando el movimiento, el consumo de maná, la animación y el sonido.
     */
    private void HandleSkill(InputAction.CallbackContext context, AudioClip sound, string animationTrigger, int manaCost)
    {
        if (context.performed && CanUseSkill())
        {
            if (manaCost > 0 && playerStats.mana < manaCost) return;

            playerMoviement.SetSkillState(true); 
            AudioManager.Instance.PlaySound(sound);
            animator.SetTrigger(animationTrigger);

            if (manaCost > 0)
            {
                playerStats.ReduceMana(manaCost);
            }
        }
        else if (context.canceled)
        {
            AudioManager.Instance.StopSound();
            playerMoviement.SetSkillState(false); 
        }
    }

    /*
     * Método: CanUseSkill.
     * Parámetros: Ninguno.
     * Descripción: Verifica si el jugador puede usar una habilidad en función de su estado actual.
     * El jugador no debe estar usando otra habilidad, debe estar en el suelo y no estar agachado.
     */
    private bool CanUseSkill()
    {
        return !playerMoviement.IsUsingSkill && playerMoviement.IsGrounded() && !playerMoviement.isCrouched;
    }
}
