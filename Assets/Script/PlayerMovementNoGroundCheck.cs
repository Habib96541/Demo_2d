using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementNoGroundCheck : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody2D rb;
    private float horizontalInput;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        var keyboard = UnityEngine.InputSystem.Keyboard.current;
        if (keyboard == null) return;
        horizontalInput = 0f;
        // LEFT: A key or Left Arrow key
        if (keyboard.aKey.isPressed ||
        keyboard.leftArrowKey.isPressed)
            horizontalInput = -1f;
        // RIGHT: D key or Right Arrow key
        if (keyboard.dKey.isPressed ||
        keyboard.rightArrowKey.isPressed)
            horizontalInput = 1f;
        // No ground check: the player can jump in the air.
        // JUMP: Space key
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
        horizontalInput * moveSpeed,
        rb.linearVelocity.y
        );
    }
}
