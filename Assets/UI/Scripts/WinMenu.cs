using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public GameObject music;

    public int currentLevel;

    [HideInInspector]private int nextLevel;

    private void Awake()
    {
        nextLevel = currentLevel + 1;
        music.GetComponent<AudioSource>().volume = 0.2f;
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0); 
    }

}
