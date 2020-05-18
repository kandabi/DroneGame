using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip FireSfx;
    public AudioClip RevDownSfx;
    public AudioClip ReloadSfx;

    public bool isFiring = false;
    public bool isRevvingDown = false;
    public bool isReloading = false;

    public float volume = 0.1f;

    public void Reload()
    {
        if(!isReloading)
        {
            isReloading = true;
            isRevvingDown = false;
            isFiring = false;
            audioSource.Stop();
            audioSource.PlayOneShot(RevDownSfx);
            audioSource.PlayOneShot(ReloadSfx);
        }
    }

    public void RevDown()
    {
        isReloading = false;
        isRevvingDown = true;
        isFiring = false;
        audioSource.Stop();
        audioSource.PlayOneShot(RevDownSfx);
    }

    public void PlayFire()
    {
        if (isRevvingDown || !audioSource.isPlaying)
        {
            isReloading = false;
            isRevvingDown = false;
            isFiring = true;
            audioSource.Stop();
            audioSource.PlayOneShot(FireSfx, volume);
        }
    }
}
