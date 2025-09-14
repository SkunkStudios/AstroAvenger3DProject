using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClip : MonoBehaviour
{
    public AudioSource music;
    public string[] exploreNames = new string[5];
    public AudioClip[] memeClips;
    public AudioSource healthAlert;
    public AudioSource wastedFlashAudio;
    public AudioSource wastedAudio;
    public AudioSource wastedTextAudio;

    private AudioSource audio;
    private  GameObject[] audiopool;
    private static int audiopoolindex;

    private void Awake()
    {
        InitAudioPool();
        audio = GetComponent<AudioSource>();
    }

    private void InitAudioPool()
    {
        if (audiopool == null)
        {
            audiopool = new GameObject[20];
            for (int i = 0; i < audiopool.Length; i++)
            {
                GameObject gameObject = new GameObject("audio pool");
                gameObject.AddComponent<AudioSource>();
                AudioSource component = gameObject.GetComponent<AudioSource>();
                component.priority = 128;
                audiopool[i] = gameObject;
            }
        }
    }

    public void PlaySound(string soundName)
    {
        GameObject gameObject = audiopool[audiopoolindex];
        AudioSource component = gameObject.GetComponent<AudioSource>();
        component.clip = Resources.Load<AudioClip>("Sound/" + soundName);
        component.pitch = Random.Range(0.8f, 1.2f);
        component.Play();
        if (++audiopoolindex == audiopool.Length)
        {
            audiopoolindex = 0;
        }
    }

    public void PlayClip(string soundName)
    {
        audio.PlayOneShot(Resources.Load<AudioClip>("Sound/" + soundName));
    }

    public void PlayMemes()
    {
        audio.PlayOneShot(memeClips[Random.Range(0, memeClips.Length)]);
    }

    public void PlayExplosion()
    {
        PlaySound(exploreNames[Random.Range(0, 5)]);
    }

    public void AudioStop()
    {
        music.volume -= 0.05f;
        audio.volume -= 0.05f;
        healthAlert.volume -= 0.05f;
        for (int i = 0; i < audiopool.Length; i++)
        {
            GameObject gameObject = audiopool[i];
            AudioSource component = gameObject.GetComponent<AudioSource>();
            component.volume -= 0.05f;
        }
    }
}
