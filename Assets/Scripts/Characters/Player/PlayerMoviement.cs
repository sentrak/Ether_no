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
    [SerializeField] private float jumpingPower = 16f; // Fuerza del salto normal del jugador
    [SerializeField] private float doubleJumpingPower = 24f; // Fuerza del salto alto para doble clic
    [SerializeField] private float doubleClickTime = 0.3f; // Tiempo para detectar doble clic
    private float lastClickTime = -1f; // Tiempo del último clic de salto

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

    private void FixedUpdate()
    {
        if (!isCrouched)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontal = input.x;
        vertical = input.y;

        // Saltar
        if (vertical > 0 && IsGrounded())
        {
            HandleJump();
        }

        // Crouch
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

    private void HandleJump()
    {
        float currentTime = Time.time;

        // Verificar si el tiempo entre clics es menor que el tiempo para un doble clic
        if (currentTime - lastClickTime <= doubleClickTime)
        {
            // Si es un doble clic, saltar más alto
            Jump(doubleJumpingPower);
            lastClickTime = -1f; // Reiniciar el tiempo
        }
        else
        {
            // Si no es doble clic, salto normal
            Jump(jumpingPower);
            lastClickTime = Time.time; // Guardar el tiempo del primer clic
        }
    }

    private void Jump(float jumpForce)
    {
        // Aplicar la fuerza de salto
        AudioManager.Instance.PlaySound(jump);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
