using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]

public class ColorBox : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BoxCollider2D boxCollider;

    public ColorState color;

    public bool isCarried = false;

    public bool onTarget;

    [HideInInspector] public bool canBeCarried = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ChangeColliderEnable(bool enable)
    {
        boxCollider.isTrigger = enable;
    }

}
