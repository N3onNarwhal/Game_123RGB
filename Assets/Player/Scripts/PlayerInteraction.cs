using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent (typeof(Rigidbody2D))]
public class PlayerInteraction : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference interactAction;
    public InputActionReference moveAction;

    [Header("Interaction Settings")]
    public LayerMask boxLayer;
    public float interactRange = 10f;
    public Transform carryPoint;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private ColorBox carriedBox;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (interactAction != null) interactAction.action.started += OnInteractPressed;
        if (moveAction != null) moveAction.action.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        if (moveAction != null) moveAction.action.canceled += ctx => moveInput = Vector2.zero;

        interactAction?.action.Enable();
        moveAction?.action.Enable();
    }

    private void OnDisable()
    {
        if (interactAction != null) interactAction.action.started -= OnInteractPressed;

        interactAction?.action.Disable();
        moveAction?.action.Disable();
    }

    private void OnInteractPressed(InputAction.CallbackContext context)
    {
        if (carriedBox == null)
        {
            TryPickupBox();
        }
        else
        {
            DropBox();
        }
    }

    private void TryPickupBox()
    {
        Vector2 dir = GetFacingDirection();
        Collider2D hit = Physics2D.OverlapCircle(rb.position, interactRange, boxLayer);

        if (hit != null)
        {
            ColorBox box = hit.GetComponent<ColorBox>();
            if (box == null) return;

            // if box is not currently being carried and it matches the player color, pick it up
            if (box.canBeCarried && box.color == GetComponent<PlayerRGBController>().currentState && !box.isCarried)
            {
                carriedBox = box;
                carriedBox.isCarried = true;
                carriedBox.canBeCarried = false;

                // Disable physics while carried
                carriedBox.rb.bodyType = RigidbodyType2D.Kinematic;
                carriedBox.rb.simulated = false;
                carriedBox.ChangeTriggerEnable(true);

                // Parent to player carry point
                carriedBox.transform.SetParent(carryPoint);
                carriedBox.transform.localPosition = Vector3.zero;

                // play pickup sound
                AudioSource pickupSound = carriedBox.GetComponent<AudioSource>();
                if (pickupSound != null)
                {
                    pickupSound.PlayOneShot(pickupSound.clip);
                }
            }
        }
    }

    private void DropBox()
    {
        if (carriedBox == null) return;

        carriedBox.isCarried = false;
        carriedBox.canBeCarried = true;
        carriedBox.transform.SetParent(null);

        // Re-enable physics
        carriedBox.rb.bodyType = RigidbodyType2D.Kinematic;
        carriedBox.rb.simulated = true;
        carriedBox.ChangeTriggerEnable(false);

        carriedBox = null;
    }

    private Vector2 GetFacingDirection()
    {
        if (moveInput.sqrMagnitude > 0.01f) return moveInput.normalized;
        return Vector2.up;
    }
}
