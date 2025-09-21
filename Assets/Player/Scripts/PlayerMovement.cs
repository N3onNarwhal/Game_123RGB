using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerControls inputActions;
    private Rigidbody2D rb;
    [SerializeField] public float moveSpeed = 5.0f;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Movement.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        inputActions.Movement.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Movement.Move.canceled -= ctx => moveInput = Vector2.zero;
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}
