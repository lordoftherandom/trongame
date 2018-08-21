using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour {

    private static bool soundPlaying = false, playSong = true;
    private static SoundHandler instance;
    private static List<AudioClip>[] soundsToPlay;
    private static List<AudioClip> soundQueue;
    private static int[] chordPosition = { 0, 0, 0 };
    public static AudioClip mainSong;
    public static AudioSource main, secondary, secondary2;

    private void Awake()
    {
        Debug.Log("SoundHandler: I'm Awake, I'm Awake");
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        LoadSounds();
    }

    private static void LoadSounds()
    {
        soundQueue = new List<AudioClip>();
        AudioSource[] sources = instance.GetComponents<AudioSource>();
        main = sources[0];
        secondary = sources[1];
        secondary2 = sources[2];
        soundsToPlay = new List<AudioClip>[3];
        for (int i = 0; i < 3; i++)
        {
            soundsToPlay[i] = new List<AudioClip>();
            for (int j = 0; j < 3; j++)
            {
                string soundPath = ((ObjType)i).ToString() + "_" + j;
                AudioClip clip;
                if ((clip = Resources.Load(soundPath, typeof(AudioClip)) as AudioClip) == null)
                    Debug.Log("Whoops, sound not working");
                soundsToPlay[i].Add(clip);
            }
        }
    }

    public static void MainSong()
    {
        main.clip = mainSong;
        while (playSong)
            if (main.isPlaying == false)
                main.Play(0);
    }

    public static void QueueSound(ObjType obj)
    {
        int currChordPos = chordPosition[(int)obj]++%3;
        chordPosition[(int)obj] = currChordPos;

        soundQueue.Add(soundsToPlay[(int)obj][currChordPos]);
        if (!soundPlaying)
            instance.StartCoroutine(PlaySound());
    }

    private static IEnumerator PlaySound()
    {
        while (soundQueue.Count > 0)
        {
            Debug.Log("About to sound");
            if (secondary.isPlaying)
                yield return new WaitForSeconds(secondary.clip.length / 2.0f);
            if (secondary.isPlaying)
            {
                secondary2.clip = soundQueue[0];
                secondary2.Play();
                soundQueue.RemoveAt(0);
            }
            else
            {
                secondary.clip = soundQueue[0];
                secondary.Play();
                soundQueue.RemoveAt(0);
            }
        }
    }

}
