using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool paused = false;

    PauseAction action;

    public GameObject menu;

    public AudioSource pauseSound;
    public AudioSource unpauseSound;

    private void Awake()
    {
        action = new PauseAction();
        ResumeGame();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    public void DisablePause()
    {
        action.Pause.PauseGame.performed -= _ => DeterminePause();
        action.Disable();
    }

    private void Start()
    {
        action.Pause.PauseGame.performed += _ => DeterminePause();
    }

    private void DeterminePause()
    {
        if (paused)
        {
            ResumeGameButton();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0.5f;
        pauseSound.Play();
        paused = true;
        menu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1.0f;
        paused = false;
        menu.SetActive(false);
    }

    public void ResumeGameButton()
    {
        unpauseSound.Play();
        ResumeGame();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
