using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerScript : MonoBehaviour
{
    [System.Serializable]
    public struct AudioMapping
    {
        public string soundName;
        public AudioSource source;
    }

    public List<AudioMapping> sounds;

    private void Awake()
    {
        if (GameObject.Find("AudioManager") is {} am)
        {
            if (am != this.gameObject)
                Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void playSound(string SoundName)
    {
        if (sounds.Count == 0)
            Debug.LogWarning("sound list contain no sounds");

        foreach (AudioMapping item in sounds)
        {
            if (item.soundName.Equals(SoundName))
            {
                item.source.Play();
                return;
            }
        }

        Debug.LogWarning("sound not found");
    }
}
