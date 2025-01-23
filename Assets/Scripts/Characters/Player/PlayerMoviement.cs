// PlayerMoviement.cs
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Clase: PlayerMoviement.
 * Descripción: Gestiona el movimiento del jugador, incluyendo caminar, saltar y agacharse. 
 *              Permite controlar el estado de habilidades activas y sincroniza las animaciones con los estados del jugador.
 */
public class PlayerMoviement : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip jump; // Clip de audio para el salto
    [SerializeField] private AudioClip walk; // Clip de audio para caminar
    [SerializeField] private AudioClip crouch; // Clip de audio para agacharse

    [Header("Player Components")]
    [SerializeField] private Rigidbody2D rb; // Referencia al Rigidbody2D para manejar la física del jugador
    [SerializeField] private Transform groundCheck; // Punto de verificación para determinar si el jugador está en el suelo
    [SerializeField] private LayerMask groundLayer; // Capa que representa los objetos considerados como "suelo"
    [SerializeField] public Animator animator; // Referencia al Animator para controlar las animaciones del jugador

    [Header("Player Variables")]
    [SerializeField] private float speed = 8f; // Velocidad de movimiento horizontal
    [SerializeField] private float jumpingPower = 16f; // Fuerza aplicada al saltar

    private float horizontal; // Movimiento horizontal del jugador
    private bool isFacingRight = true; // Indica si el jugador está mirando hacia la derecha
    public bool isCrouched { get; private set; } = false; // Indica si el jugador está agachado
    public bool IsUsingSkill { get; set; } = false; // Indica si el jugador está usando una habilidad

    /*
     * Método: Update.
     * Parámetros: Ninguno.
     * Descripción: Gestiona el cambio de dirección y actualiza las animaciones del jugador, excepto cuando está usando una habilidad.
     */
    private void Update()
    {
        if (IsUsingSkill) return;
        HandleFlip();
        UpdateAnimatorParameters();
    }

    /*
     * Método: FixedUpdate.
     * Parámetros: Ninguno.
     * Descripción: Aplica el movimiento horizontal al Rigidbody2D si el jugador no está agachado ni usando una habilidad.
     */
    private void FixedUpdate()
    {
        if (!isCrouched && !IsUsingSkill)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }

    /*
     * Método: Move.
     * @param context: Contexto del Input System para capturar las entradas del jugador.
     * Descripción: Maneja el movimiento horizontal, el salto y el agacharse según la entrada del jugador.
     */
    public void Move(InputAction.CallbackContext context)
    {
        if (IsUsingSkill) return;

        Vector2 input = context.ReadValue<Vector2>();
        horizontal = input.x;
        if (input.y > 0 && IsGrounded())
        {
            Jump();
        }
        else if (input.y < 0 && IsGrounded())
        {
            rb.linearVelocity = Vector2.zero;
            AudioManager.Instance.PlaySound(crouch);
            Crouch();
        }
        else
        {
            StopCrouching();
        }
    }

    /*
     * Método: SetSkillState.
     * @param isUsingSkill: Indica si el jugador está utilizando una habilidad.
     * Descripción: Activa o desactiva el estado de uso de habilidad, deteniendo el movimiento y la física si es necesario.
     */
    public void SetSkillState(bool isUsingSkill)
    {
        IsUsingSkill = isUsingSkill;
        if (isUsingSkill)
        {
            horizontal = 0;
            rb.linearVelocity = Vector2.zero;
        }
    }

    /*
     * Método: HandleFlip.
     * Parámetros: Ninguno.
     * Descripción: Cambia la dirección en la que mira el jugador según el movimiento horizontal.
     */
    private void HandleFlip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    /*
     * Método: UpdateAnimatorParameters.
     * Parámetros: Ninguno.
     * Descripción: Actualiza los parámetros del Animator según el estado actual del jugador.
     */
    private void UpdateAnimatorParameters()
    {
        animator.SetBool("isRunning", Mathf.Abs(horizontal) > 0);
        animator.SetBool("isJumping", !IsGrounded());
        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("isFalling", rb.linearVelocity.y < 0 && !IsGrounded());
    }

    /*
     * Método: Jump.
     * Parámetros: Ninguno.
     * Descripción: Aplica una fuerza hacia arriba para hacer que el jugador salte.
     */
    private void Jump()
    {
        AudioManager.Instance.PlaySound(jump);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
    }

    /*
     * Método: Crouch.
     * Parámetros: Ninguno.
     * Descripción: Activa el estado de agachado del jugador, deteniendo su movimiento horizontal.
     */
    private void Crouch()
    {
        horizontal = 0;
        isCrouched = true;
        animator.SetBool("isCrounched", true);
    }

    /*
     * Método: StopCrouching.
     * Parámetros: Ninguno.
     * Descripción: Desactiva el estado de agachado, permitiendo que el jugador vuelva a moverse.
     */
    private void StopCrouching()
    {
        isCrouched = false;
        animator.SetBool("isCrounched", false);
    }

    /*
     * Método: IsGrounded.
     * Parámetros: Ninguno.
     * Descripción: Verifica si el jugador está tocando el suelo utilizando un OverlapCircle.
     */
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
