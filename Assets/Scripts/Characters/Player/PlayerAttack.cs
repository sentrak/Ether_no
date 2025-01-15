using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip punch, shild, cross; // Clips de audio

    [Header("Player Components")]
    public Animator animator; // Referencia al Animator para manejar las animaciones del jugador
    private PlayerStats playerStats; // Referencia al script PlayerStats para manejar vida y maná
    private PlayerMoviement playerMoviement; // Referencia al script PlayerMoviement para manejar el movimiento del jugador

    [Header("Mana Settings")]
    [SerializeField] private int crossManaCost = 10; // Costo de maná para realizar el ataque especial "Cross"
    [SerializeField] private int shildManaCost = 5; // Costo de maná para usar el escudo

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias a los scripts PlayerStats y PlayerMoviement.
     */
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        playerMoviement = GetComponent<PlayerMoviement>();
    }

    /*
     * Método: PerformAttack.
     * @param context: Contexto del evento de ataque del Input System.
     * Descripción: Maneja el ataque del jugador activando la animación y sonido correspondientes.
     */
    public void PerformAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameObject.tag = "Untagged";
            AudioManager.Instance.PlaySound(punch); 
            animator.SetTrigger("isAttacking"); 
            playerMoviement.horizontal = 0; 
            playerMoviement.vertical = 0; 
        }
        else if (context.canceled)
        {
            AudioManager.Instance.StopSound();
            gameObject.tag = "Player";

        }
    }

    /*
     * Método: UseShield.
     * @param context: Contexto del evento de escudo del Input System.
     * Descripción: Maneja la activación del escudo y permite gastar maná mientras el escudo esté activado.
     */
    public void UseShield(InputAction.CallbackContext context)
    {
        if (context.performed && playerStats.mana >= shildManaCost)
        {
            gameObject.tag = "Untagged";
            AudioManager.Instance.PlaySound(shild); 
            animator.SetTrigger("isSheltering");
            playerMoviement.horizontal = 0; 
            playerMoviement.vertical = 0; 
            playerStats.UseMana(shildManaCost); 
        }
        else if (context.canceled)
        {
            AudioManager.Instance.StopSound();
            gameObject.tag = "Player";
        }
    }

    /*
     * Método: PerformCross.
     * @param context: Contexto del evento de cruz del Input System.
     * Descripción: Maneja el ataque especial "Cross", activando la animación, sonido y consumiendo maná.
     */
    public void PerformCross(InputAction.CallbackContext context)
    {
        if (context.performed && playerStats.mana >= crossManaCost)
        {
            gameObject.tag = "Untagged";
            AudioManager.Instance.PlaySound(cross); 
            animator.SetTrigger("isShooting"); 
            playerMoviement.horizontal = 0; 
            playerMoviement.vertical = 0; 
            playerStats.UseMana(crossManaCost); 
        }
        else if (context.canceled)
        {
            AudioManager.Instance.StopSound(); 
            gameObject.tag = "Player";

        }
    }
}
