using UnityEngine;

public class MusicManager : MonoBehaviour
{

    // reference to music clips
    public Music[] songs;
    private string currentMusicID;

    // set volume in Inspector
    [SerializeField] public float musicVolume;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        // create audio sources
        foreach (Music song in songs)
        {
            song.source = gameObject.AddComponent<AudioSource>();
            song.source.clip = song.clip;
        }

        ChangeMusic("Menu");
    }

    private void OnEnable()
    {
        
    }

    public void ChangeMusic(string newMusicID)
    {
        // stop current music
        foreach (Music song in songs)
        {
            if (song.musicID == currentMusicID)
            {
                song.source.Stop();
                break;
            }
        }

        // change current music id
        currentMusicID = newMusicID;

        // play new music
        foreach (Music song in songs)
        {
            if (song.musicID == currentMusicID)
            {
                song.source.loop = true;
                song.source.playOnAwake = true;
                song.source.volume = musicVolume;

                song.source.Play();
                break;
            }
        }
    }
}
