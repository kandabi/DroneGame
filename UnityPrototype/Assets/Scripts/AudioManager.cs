using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip FireSfx;
    public AudioClip RevDownSfx;

    public bool isFiring = false;

    public float volume = 0.1f;

    public void RevDown()
    {
        isFiring = false;
        audioSource.Stop();
        audioSource.PlayOneShot(RevDownSfx);
    }

    public void PlayFire()
    {
        if (!audioSource.isPlaying)
        {
            isFiring = true;
            audioSource.PlayOneShot(FireSfx, volume);
        }

    }
}
