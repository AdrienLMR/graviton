using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    private FMOD.Studio.EventInstance musicMain;



    void Start()
    {
        musicMain = FMODUnity.RuntimeManager.CreateInstance("event:/MUSIC/sound_music_main");
        PlayMusic();
    }

    public void PlayMusic()
    {
        musicMain.start();
    }

    public void StopMusic()
    {
        musicMain.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
