using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour {
    private static bool soundPlaying = false, playSong = true;
    private static SoundHandler instance;
    static List<AudioClip> soundsToPlay;
    public static AudioClip mainSong;
    public static AudioSource main, secondary;

    private void Awake()
    {
        Debug.Log("SoundHandler: I'm Awake, I'm Awake");
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        AudioSource[] sources = instance.GetComponents<AudioSource>();
        main = sources[0];
        secondary = sources[1];
        soundsToPlay = new List<AudioClip>();
    }

    public static void MainSong()
    {
        main.clip = mainSong;
        while (playSong)
            if (main.isPlaying == false)
                main.Play(0);
    }

    public static void QueueSound(AudioClip sound)
    {
        if (soundsToPlay == null)
            soundsToPlay = new List<AudioClip>();

        soundsToPlay.Add(sound);
        if (!soundPlaying)
            instance.StartCoroutine(PlaySound());
    }

    private static IEnumerator PlaySound()
    {
        while (soundsToPlay.Count > 0)
        {
            Debug.Log("About to sound");
            if(secondary.isPlaying)
                yield return new WaitForSeconds(secondary.clip.length/1.25f);
            secondary.clip = soundsToPlay[0];
            secondary.Play();
            soundsToPlay.RemoveAt(0);
        }
    }

}
