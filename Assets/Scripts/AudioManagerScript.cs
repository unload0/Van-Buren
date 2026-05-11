using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript Instance;

    [System.Serializable]
    public struct AudioMapping
    {
        public string name;
        public AudioClip soundClip;
        public AudioMixerGroup mixerGroup;
    }

    private AudioSource audioSource;

    public List<AudioMapping> sounds;

    private void Awake()
    {
        if (GameObject.Find("AudioManager") is { } am)
        {
            if (am != this.gameObject)
                Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        audioSource = this.GetComponent<AudioSource>();

        if (Instance == null)
            Instance = this;
    }

    public void playSound(string SoundName)
    {
        if (sounds.Count == 0)
            Debug.LogWarning("sound list contain no sounds");
        
        if (sounds.FirstOrDefault(s => s.name == SoundName) is { soundClip: not null } mapping)
        {
            audioSource.outputAudioMixerGroup = mapping.mixerGroup;
            audioSource.PlayOneShot(mapping.soundClip);
        }
        else
        {
            Debug.LogWarning("sound not found");    
        }
    }
}
