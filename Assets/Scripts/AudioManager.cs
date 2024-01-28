using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SFX
{
    public AudioClip[] sfx;
}

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource soundEffect;

    public AudioClip backgroundMusicClip;
    
    public SFX[] soundEffectClips;
    //AudioManager.Instance.PlaySoundEffect(0);

    private static AudioManager instance;
    void Start()
    {
        PlayBackgroundMusic();
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, set the instance to this and mark it to not be destroyed on scene load
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }


    public void PlayBackgroundMusic()
    {
        if (backgroundMusicClip != null)
        {
            backgroundMusic.clip = backgroundMusicClip;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }

    public void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffectClips.Length)
        {
            int i = soundEffectClips[index].sfx.Length;
            soundEffect.PlayOneShot(soundEffectClips[index].sfx[Random.Range(0, i)]);
            //soundEffect.clip = soundEffectClips[index];
            //soundEffect.PlayOneShot();
        }
    }

    public void SetVolume(float volume)
    {
        // Implement volume control logic here
    }
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "AudioManager";
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }
}
