using UnityEngine;

public class UiSoundManager : MonoBehaviour
{
    public void PlayButtonSound(AudioSource audio)
    {
        Debug.Log("Sound should play");
        audio.Play();
    }
}
