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

        //if (SceneManager.GetActiveScene().buildIndex == 1)
        //{
        //    canWin = true;
        //}

        boxCollider = GetComponent<BoxCollider2D>();
        colorBoxes = Object.FindObjectsByType<ColorBox>(FindObjectsSortMode.None);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Win trigger collision");

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

        // if none of the boxes are off-target, return true
        return true;
    }

    private void WinLevel()
    {
        // trigger win
        boxCollider.isTrigger = true;

        // disable player functionality
        pauseManager.DisablePause();

        PlayerRGBController colorController = player.GetComponent<PlayerRGBController>();
        if (colorController != null) colorController.DisableColors();

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null) playerMovement.DisableMovement();

        // open win menu
        winMenu.SetActive(true);

        winSound.PlayOneShot(winSound.clip);
    }

}
