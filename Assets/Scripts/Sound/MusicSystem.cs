using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    private FMOD.Studio.EventInstance musicMain;
    public static MusicSystem Instance;
    
    private void Start()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
        PlayMusic();
    }

    public void PlayMusic()
    {
        musicMain = FMODUnity.RuntimeManager.CreateInstance("event:/MUSIC/sound_music_main");
        
        if (!IsPlaying(musicMain))
            musicMain.start();
    }

    public void StopMusic()
    {
        musicMain.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public static bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
