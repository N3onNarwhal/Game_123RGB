using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public GameObject music;

    public int currentLevel;
    private int maxLevel = 4;

    [HideInInspector]private int nextLevel;

    private void Awake()
    {
        nextLevel = currentLevel + 1;
        music.GetComponent<AudioSource>().volume = 0.2f;
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        if (nextLevel > maxLevel)
        {
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0); 
    }

}
