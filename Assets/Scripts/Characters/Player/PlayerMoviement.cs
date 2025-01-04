using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMoviement : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip jump; // Clip de audio para el salto

    [Header("Player Components")]
    public Rigidbody2D rb; // Componente Rigidbody2D del jugador para manejar la física
    public Transform groundCheck; // Transform usado para verificar si el jugador está tocando el suelo
    public LayerMask groundLayer; // Capa que define qué objetos se consideran "suelo"
    public Animator animator; // Referencia al Animator para manejar las animaciones del jugador

    [Header("Player Variables")]
    public float horizontal; // Almacena el valor del movimiento horizontal del jugador
    public float vertical; // Almacena el valor del movimiento vertical del jugador
    private bool isFacingRight = true; // Indica si el jugador está mirando hacia la derecha
    private bool isCrouched = false; // Indica si el jugador está agachado
    private bool isRunning = false; // Indica si el jugador está corriendo

    [SerializeField] private float speed = 8f; // Velocidad de movimiento horizontal del jugador
    [SerializeField] private float jumpingPower = 16f; // Fuerza del salto del jugador

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Controla el flip del personaje, actualiza los parámetros del Animator y gestiona el estado de agachado, salto y correr.
     */
    void Update()
    {
        if (!isFacingRight && horizontal > 0f) Flip();
        else if (isFacingRight && horizontal < 0f) Flip();
        isRunning = Mathf.Abs(horizontal) > 0;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", !IsGrounded());
        animator.SetBool("isGrounded", IsGrounded());
        if (rb.linearVelocity.y > 0)
        {
            animator.SetBool("isFalling", false);
        }
        else if (rb.linearVelocity.y < 0 && !IsGrounded())
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }
    }

    /*
     * Método: FixedUpdate.
     * Parámetros: Ninguno.
     * Descripción: Aplica la física del movimiento del jugador y bloquea el movimiento horizontal si está agachado.
     */
    private void FixedUpdate()
    {
        if (!isCrouched)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }

    /*
     * Método: Move.
     * @param context: Contexto del evento de movimiento del Input System.
     * Descripción: Captura el valor del movimiento horizontal y vertical desde el Input System y maneja el salto.
     */
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontal = input.x;
        vertical = input.y;
        //jump
        if (vertical > 0 && IsGrounded())
        {
            AudioManager.Instance.PlaySound(jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        //Crounch
        if (vertical < 0 && IsGrounded())
        {
            horizontal = 0;
            animator.SetBool("isCrounched", true);
        }
        else
        {
            animator.SetBool("isCrounched", false);
        }
    }

    /*
     * Método: Flip.
     * Parámetros: Ninguno.
     * Descripción: Invierte la dirección del personaje en el eje X.
     */
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    /*
     * Método: IsGrounded.
     * Parámetros: Ninguno.
     * Descripción: Verifica si el jugador está tocando el suelo usando un OverlapCircle.
     */
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
