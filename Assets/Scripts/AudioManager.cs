using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    
    public AudioClip snapped;
    public AudioClip click;
    public AudioClip pop;
    

    

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    
    }

    

    
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && !sfxSource.isPlaying)
            sfxSource.PlayOneShot(clip);
    }
    public void PlaySnappedSfx()
    {
        PlaySFX(snapped);
    }
    public void PlayClickSfx()
    {
        PlaySFX(click);
    }
    public void PlayPopSfx()
    {
        PlaySFX(pop);
    }
    
}
