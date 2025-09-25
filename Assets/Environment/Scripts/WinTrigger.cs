using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class WinTrigger : MonoBehaviour
{
    public ColorBox[] colorBoxes;

    public BoxCollider2D boxCollider;

    public PauseManager pauseManager;
    public GameObject winMenu;
    public GameObject player;

    public AudioSource winSound;

    bool canWin = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        colorBoxes = Object.FindObjectsByType<ColorBox>(FindObjectsSortMode.None);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canWin = CheckWinConditions();

        if (canWin)
        {
            WinLevel();
        }
    }

    private bool CheckWinConditions()
    {
        // check if boxes are on their targets
        foreach (var box in colorBoxes)
        { 
            if (!box.onTarget)
            {
                return false;
            }
        }

        // if all of the boxes are on a target, return true
        return true;
    }

    private void WinLevel()
    {
        // trigger win
        boxCollider.isTrigger = true;

        // disable player functionality
        pauseManager.DisablePause();

        // open win menu
        winMenu.SetActive(true);
        winSound.PlayOneShot(winSound.clip);
    }

}
