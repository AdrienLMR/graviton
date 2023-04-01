using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    private FMOD.Studio.EventInstance musicMain;
    public static MusicSystem Instance;
    

    void Start()
    {
        if (Instance != null && Instance != this)
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
        {
            Debug.Log("a");
            musicMain.start();
        }
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
