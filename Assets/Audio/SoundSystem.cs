using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [System.Serializable]
    public class SoundLibrary
    {
        public AudioClip clickButton, deadPlayer, upgradePlayer, startKickPlayer, kickPlayer, rolling, levelUp, fire, burp, fart, kickTotarget;
    }
    public static SoundSystem instance;
    
    public SoundLibrary soundLibrary;
    [SerializeField] private Sound soundPrefab;
    [SerializeField] private AudioSource backgroundAudioSource;
    public bool isAudioPlay;

    public void Init()
    {
        InitSingleton();
    }
    private void InitSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            isAudioPlay = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetActiveSound(bool active)
    {
        isAudioPlay = active;
        if(isAudioPlay == false)
        {
            backgroundAudioSource.Stop();
        }
        else
        {
            backgroundAudioSource.Play();
        }
    }
    public void CreateSound(AudioClip audioClip)
    {
        if (isAudioPlay)
        {
            var sound = Instantiate(soundPrefab, transform);
            sound.PlaySound(audioClip);
        }

    }
    public void OnApplicationPause(bool pause)
    {
        Debug.Log(pause + " paused");
    }
    public void OnApplicationFocus(bool focus)
    {
        Debug.Log(focus + " focus");
    }
   
}
