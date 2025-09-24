using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]

public class ColorBox : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BoxCollider2D boxCollider;

    public bool isCarried = false;

    public bool onTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        boxCollider = GetComponent<BoxCollider2D>();
        ChangeColliderEnable(true);

    }

    public void ChangeColliderEnable(bool enable)
    {
        boxCollider.enabled = enable;
    }

}
