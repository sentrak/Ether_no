using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviement : MonoBehaviour
{
      [Header("Audio Sources")]
        [SerializeField] private AudioClip jump;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator; 

    public float horizontal;
    private float vertical; // Para capturar el movimiento vertical
    private bool isFacingRight = true; // Dirección del personaje
    private bool isCrouched = false; // Estado de agachado
    private bool isRunning = false; // Estado de correr

    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;

    void Update()
    {
        /* Método: Update.
         * Parámetros: Ninguno.
         * Descripción: Controla el flip del personaje, actualiza los parámetros del Animator y gestiona el estado de agachado, salto y correr.
         */

        // Controlar el flip del personaje
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }

        // Actualizar parámetros del Animator
        isRunning = Mathf.Abs(horizontal) > 0; // El personaje está corriendo si el movimiento horizontal es distinto de 0
        animator.SetBool("isRunning", isRunning); // Estado de correr
        animator.SetBool("isJumping", !IsGrounded()); // Estado de salto
        animator.SetBool("isGrounded", IsGrounded()); // Contacto con el suelo

        // Diferenciar entre subida y caída
        if (rb.linearVelocityY > 0) // Movimiento hacia arriba
        {
            animator.SetBool("isFalling", false);
        }
        else if (rb.linearVelocityY < 0 && !IsGrounded()) // Movimiento hacia abajo y no en el suelo
        {
            animator.SetBool("isFalling", true);
        }
        else // En el suelo o sin movimiento vertical
        {
            animator.SetBool("isFalling", false);
        }

        // Activar o desactivar el estado de "isCrounched"
        isCrouched = vertical < 0 && IsGrounded();
        animator.SetBool("isCrounched", isCrouched);
    }

    private void FixedUpdate()
    {
        /* Método: FixedUpdate.
         * Parámetros: Ninguno.
         * Descripción: Aplica la física del movimiento del jugador y bloquea el movimiento horizontal si está agachado.
         */
        if (!isCrouched)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocityY);
        }
    }

    /* Método: Move.
     * Parámetros:
     * @param context: Contexto del evento de movimiento del Input System.
     * Descripción: Captura el valor del movimiento horizontal y vertical desde el Input System y maneja el salto.
     */
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontal = input.x;
        vertical = input.y;
        if (vertical > 0 && IsGrounded())
        {
            AudioManager.Instance.PlaySound(jump);

            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpingPower);
        }
    }

    /* Método: Flip.
     * Parámetros: Ninguno.
     * Descripción: Invierte la escala horizontal del jugador para reflejarlo hacia la dirección correcta.
     */
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    /* Método: IsGrounded.
     * Parámetros: Ninguno.
     * Descripción: Verifica si el jugador está en contacto con el suelo usando un OverlapCircle.
     */
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
