using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip audioClip2;

    public void AttackClip()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void DamageClip()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip2);
        }
    }
}
