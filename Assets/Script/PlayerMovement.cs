using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private SpriteRenderer sd;
    private Animator an;

    // Use Rigidbody2D for 2D, or change to Rigidbody for 3D
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool isFaceRight = true;

    // New Input System references
    [Header("Input Actions")]
    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction jumpAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (sd == null) sd = GetComponentInChildren<SpriteRenderer>();
        an = GetComponentInChildren<Animator>();

        // Default WASD / Arrow keys if not set via Inspector
        if (moveAction.bindings.Count == 0)
        {
            moveAction = new InputAction("Move", binding: "<Gamepad>/leftStick");
            moveAction.AddCompositeBinding("Dpad")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
        }

        // Default Spacebar / Gamepad south button
        if (jumpAction.bindings.Count == 0)
        {
            jumpAction = new InputAction("Jump", binding: "<Keyboard>/space");
            jumpAction.AddBinding("<Gamepad>/buttonSouth");
        }
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    private void Update()
    {
        an.SetFloat("Speed", Mathf.Abs(moveInput.x));
        // Read movement as a Vector2
        moveInput = moveAction.ReadValue<Vector2>();

        // Check if jump button was pressed this frame
        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Flip character based on horizontal input
        if (moveInput.x > 0.05f && !isFaceRight)
        {
            Flip();
        }
        else if (moveInput.x < -0.05f && isFaceRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        // In Unity 2022 and older, replace linearVelocity with velocity
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    public void Flip()
    {
        isFaceRight = !isFaceRight;
        if (sd != null) sd.flipX = !isFaceRight;
    }
}