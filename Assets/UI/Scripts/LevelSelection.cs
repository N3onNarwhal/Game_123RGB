using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{

    [SerializeField] private bool unlocked;
    public Button levelButton;

    private void Update()
    {
        UpdateLevelImage();
    }

    private void UpdateLevelImage()
    {
        // if level is locked
        if (!unlocked)
        {
            levelButton.interactable = false;
        }
        // if level is unlocked
        else
        {
            levelButton.interactable = true;
        }
    }

    public void LoadLevel(int levelIndex)
    {
        if (unlocked)
        {
            SceneManager.LoadScene(levelIndex);
        }
    }

}
