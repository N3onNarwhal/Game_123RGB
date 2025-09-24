using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class BoxTarget : MonoBehaviour
{
    private BoxCollider2D col;
    private ColorBox box;

    public Transform storePoint;
    public ColorState targetColor;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // if collision is not a box
        if (!collision.CompareTag("Box")) return;

        box = collision.GetComponent<ColorBox>();
        if (box == null) return;

        if (box.color == targetColor)
        {
            // attach box to center of target
            box.transform.SetParent(storePoint);
            box.transform.localPosition = Vector3.zero;

            box.ChangeColliderEnable(true);

            // disable its ability to be picked up
            box.canBeCarried = false;
            box.onTarget = true;
        }
    }

}
