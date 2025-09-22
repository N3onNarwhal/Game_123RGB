using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColorBarrier : MonoBehaviour
{
    public enum Axis { LocalX, LocalY }

    [Header("Assign colors for each side of the barrier")]
    // on local +x side
    [SerializeField] private ColorState sideAColor;
    // on local -x side
    [SerializeField] private ColorState sideBColor;

    [Header("Colliders")]
    // collider that blocks the player, must NOT be a trigger
    [SerializeField] private Collider2D blockCollider;
    // trigger collider used to detect player presence and side
    [SerializeField] private Collider2D sensorCollider;

    [Header("Behavior")]
    [SerializeField] private Axis barrierAxis = Axis.LocalX;
    [SerializeField] private float deadzone = 0.02f;
    private string playerTag = "Player";
    bool playerInside;

    private PlayerRGBController trackRGBController;
    private Transform trackTransform;

    void Reset()
    {
        // Try to auto-assign colliders if the GameObject has two of them
        var cols = GetComponents<Collider2D>();
        if (cols.Length > 0 && blockCollider == null) blockCollider = cols[0];
        if (cols.Length > 1 && sensorCollider == null) sensorCollider = cols[1];

        // make the sensor a trigger if it exists
        if (sensorCollider != null) sensorCollider.isTrigger = true;
    }

    private void Awake()
    {
        if (blockCollider == null)
            Debug.LogError($"ColorBarrier '{name}' needs a block Collider2D assigned.");

        if (sensorCollider != null)
            sensorCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(playerTag)) return;

        trackRGBController = collision.GetComponent<PlayerRGBController>();
        trackTransform = collision.transform;
        playerInside = true;

        if (trackRGBController != null)
        {
            trackRGBController.OnColorChanged += OnColorStateChanged;
        }

        EvaluateBarrier();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag(playerTag)) return;

        // constantly re-evaluate barrier while inside sensor
        EvaluateBarrier();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(playerTag)) return;

        if (trackRGBController != null)
        {
            trackRGBController.OnColorChanged -= OnColorStateChanged;
        }

        trackRGBController = null;
        trackTransform = null;
        playerInside = false;

        if (blockCollider != null)
        {
            blockCollider.enabled = true;
        }
    }

    private void OnColorStateChanged(ColorState newState)
    {
        EvaluateBarrier();
    }

    private void EvaluateBarrier()
    {
        if (trackTransform == null || trackRGBController == null || blockCollider == null)
        {
            return;
        }

        // calculate if barrier is on side a or b
        Vector3 local = transform.InverseTransformPoint(trackTransform.position);

        float axisValue = (barrierAxis == Axis.LocalX) ? local.x : local.y;

        // local < -deadzone => Side A (left/bottom)
        // local > +deadzone => Side B (right/top)
        // if within deadzone, do not change state to avoid jitter

        ColorState destinationColor;
        if (axisValue < -deadzone)
        {
            destinationColor = sideBColor;
        }
        else if (axisValue > deadzone)
        {
            destinationColor = sideAColor;
        }
        else
        {
            return;
        }

        bool shouldBlock = trackRGBController.currentState != destinationColor;

        if (playerInside)
        {
            if (!shouldBlock)
            {
                blockCollider.enabled = false;
            }
        }
        else
        {
            blockCollider.enabled = shouldBlock;
        }
        //blockCollider.enabled = shouldBlock;
    }

}
