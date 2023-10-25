using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------- Audio Source -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------------- Audio Clip --------------")]
    public AudioClip shot1;
    public AudioClip shot2;
    public AudioClip dryShot;
    public AudioClip knockDown;
    public AudioClip scream;
    public AudioClip starting;


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
