using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour {

    public class SoundThing
    {
        public SoundThing(AudioClip clip, int mod)
        {
            sound = clip;
            pitch = mod;
        }
        public AudioClip sound;
        public int pitch;
    };

    private const float INITAL_BEATDUR = 4;
	private static bool soundPlaying = false, playSong = true,
		beatPlaying = false, shouldPlay = true;
    private static SoundHandler instance;
    private static List<AudioClip>[] soundsToPlay;
    private static List<SoundThing> soundQueue;
	private static int[] chordPosition;
	private static float[] beatTime;
	private static AudioSource[] beatSounds;
    public static AudioClip mainSong;
    public static AudioSource main, secondary, secondary2;

    private void Awake()
    {
        Debug.Log("SoundHandler: I'm Awake, I'm Awake");
        if (instance == null)
            instance = this;
        else
            Destroy(this);
		chordPosition = new int[Objs.GetTotalObjs()];
		for (int i = 0; i < chordPosition.Length; i++)
			chordPosition[i] = 0;
        LoadSounds();
    }

    private static void LoadSounds()
    {
        soundQueue = new List<SoundThing>();
        AudioSource[] sources = instance.GetComponents<AudioSource>();
        main = sources[0];
        secondary = sources[1];
        secondary2 = sources[2];
        soundsToPlay = new List<AudioClip>[Objs.GetTotalObjs()];
        for (int i = 0; i < Objs.GetTotalObjs(); i++)
        {
            soundsToPlay[i] = new List<AudioClip>();
			bool noMoreVal = false;
			int count = 0;
			while(!noMoreVal)
			{ 
                string soundPath = ((ObjType)i).ToString() + "_" + count++;
                AudioClip clip;
				if ((clip = Resources.Load(soundPath, typeof(AudioClip)) as AudioClip) == null)
				{
					noMoreVal = true;
					break;
				}
				else
					soundsToPlay[i].Add(clip);
            }
        }

		//Initalize beat sounds. beats are introduced as spawners are added
		//There should always be ObjType + 1 beat sounds; 1 for each spawner, and
		//one that plays in the background at all times
		beatSounds = new AudioSource[Objs.GetTotalObjs()+1];
		beatTime = new float [beatSounds.Length];
		for (int i = 0; i < beatSounds.Length; i++)
		{
			if((beatSounds[i] = instance.gameObject.AddComponent<AudioSource>() as AudioSource) == null)
				Debug.Log("Error Adding AudioSource Component");
			string soundPath = ("spwn_" + i);
			AudioClip clip;
			if ((clip = Resources.Load(soundPath, typeof(AudioClip)) as AudioClip) == null)
				Debug.Log("Oh no an error!");
			else
				beatSounds[i].clip = clip;
			beatSounds[i].volume = 0.5f;
			beatTime[i] = 0;
		}
		beatTime[beatSounds.Length - 1] = INITAL_BEATDUR;
		StartBeats();
    }

    public static void PauseAllSounds()
    {
		shouldPlay = !shouldPlay;
    }

    public static void MainSong()
    {
        main.clip = mainSong;
        while (playSong)
            if (main.isPlaying == false)
                main.Play(0);
    }

    public static void QueueSound(ObjType obj, int speed)
    {
        int currChordPos = chordPosition[(int)obj]++%3;
        int pitch = speed + 1 - (int)Map.GetMinSpeed();
        soundQueue.Add(new SoundThing(soundsToPlay[(int)obj][currChordPos], pitch));
        if (!soundPlaying)
            instance.StartCoroutine(PlaySound());
    }

    private static IEnumerator PlaySound()
    {
        while (soundQueue.Count > 0)
        {
			soundPlaying = true;
			while(!shouldPlay)
				yield return new WaitForSeconds(INITAL_BEATDUR/16);
            if (secondary.isPlaying)
                yield return new WaitForSeconds(secondary.clip.length / 1.25f);
            if (secondary.isPlaying)
            {
                secondary2.clip = soundQueue[0].sound;
                secondary2.pitch = soundQueue[0].pitch;
                secondary2.Play();
                soundQueue.RemoveAt(0);
            }
            else
            {
                secondary.clip = soundQueue[0].sound;
                secondary.pitch = soundQueue[0].pitch;
                secondary.Play();
                soundQueue.RemoveAt(0);
            }
        }
		soundPlaying = false;
    }

	//Adds/Removes a beat from the handler
	public static void BeatHandler(int obj, bool toAdd = true)
	{
		if (toAdd)
			if (beatTime[obj] > 0)
				beatTime[obj] /= 2;
			else
				beatTime[obj] = INITAL_BEATDUR;
		else
			beatTime[obj] *= 2;
		//If there are no longer any spawners for a given obj...
		if (beatTime[obj] > INITAL_BEATDUR)
			beatTime[obj] = 0;
		//But if there are so many objects that the beat would become too fast...
		else if (beatTime[obj] < beatSounds[obj].clip.length)
			beatTime[obj] = beatSounds[obj].clip.length; //THIS WILL CAUSE ERRORS WHERE SPAWNER EXSISTS, BUT STOPS PLAYING SOUND
	}
	
	//Starts the beats that can play at the start of the game.
	//Could possible be optamized by only starting IEnumerator when spawner spawns.
	private static void StartBeats()
	{
		for(int i = 0; i < beatSounds.Length; i++)
		{
			instance.StartCoroutine(PlayBeat(i));
		}
	}

	private static IEnumerator PlayBeat(int beat)
	{
		//Start a small offset for each beat except last beat so they don't overlap
		if (beat < beatSounds.Length - 2)
			yield return new WaitForSeconds(beat/INITAL_BEATDUR);
		//used to determine how much extra time has been used to wait for things to play
		float beatTimeOffset;
		while(true)
		{
			beatTimeOffset = 0;
			//Wait for the object spawner to spawn. Only start beat at the begining of a
			//"beat" period
			while(beatTime[beat] == 0)
				yield return new WaitForSeconds(INITAL_BEATDUR);
			while (beatPlaying)
			{
				beatTimeOffset += beatSounds[beat].clip.length;
				yield return new WaitForSeconds(beatSounds[beat].clip.length);
			}
			//Hey look! A critical section!
			beatPlaying = true;
			beatSounds[beat].Play();
			yield return new WaitForSeconds(beatSounds[beat].clip.length);
			beatTimeOffset += beatSounds[beat].clip.length;
			beatPlaying = false;

			yield return new WaitForSeconds(beatTime[beat] - beatTimeOffset);
		}
	}
}
