using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class BoxTarget : MonoBehaviour
{
    private BoxCollider2D col;
    private ColorBox box;
    private bool occupied = false;

    public Transform storePoint;
    public ColorState targetColor;
    public AudioSource audioSource;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (occupied) return;

        // if collision is not a box
        if (!collision.CompareTag("Box")) return;

        box = collision.GetComponent<ColorBox>();
        if (box == null) return;

        if (box.color == targetColor)
        {
            // attach box to center of target
            box.transform.SetParent(storePoint);
            box.transform.localPosition = Vector3.zero;
            if (audioSource != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }

            box.ChangeTriggerEnable(true);

            // disable its ability to be picked up
            box.canBeCarried = false;
            box.onTarget = true;

            // disable this target's ability to hold another box
            occupied = true;
        }
    }

}
