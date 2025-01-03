using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClip : MonoBehaviour
{
    public AudioSource[] exploreAudios;
    public AudioSource[] exploreRocetAudios;
    public AudioSource[] lazerHitAudios;
    public AudioSource[] WTFBoomAudios;
    public AudioClip[] memeClips;

    private AudioSource audio;

	void Start ()
	{
		audio = GetComponent<AudioSource>();
	}

    public void PlayMemes()
    {
        audio.PlayOneShot(memeClips[Random.Range(0, memeClips.Length)]);
    }

    public void PlayExplosion()
    {
        exploreAudios[Random.Range(0, exploreAudios.Length)].Play();
    }

    public void PlayExplosionRocket()
    {
        exploreRocetAudios[Random.Range(0, exploreRocetAudios.Length)].Play();
    }

    public void PlayLazerHit()
    {
        lazerHitAudios[Random.Range(0, lazerHitAudios.Length)].Play();
    }

    public void PlayWTFBoom()
    {
        WTFBoomAudios[Random.Range(0, WTFBoomAudios.Length)].Play();
    }

    public void PlaySound(string soundName)
    {
        audio.PlayOneShot(Resources.Load<AudioClip>("Sound/" + soundName));
    }
}
