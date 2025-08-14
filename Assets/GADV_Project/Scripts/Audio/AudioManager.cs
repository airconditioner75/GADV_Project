using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // One AudioSource to play everything
    public AudioSource audioSourceChopping;
    public AudioSource audioSourceKneading;
    public AudioSource audioSourceFrying;
    public AudioSource audioSourceGrilling;
    public AudioSource audioSourceFinish;
    public AudioSource audioSourceLose;
    public AudioSource audioSourceWin;

    // AudioClips set in the Inspector
    public AudioClip choppingClip;
    public AudioClip kneadingClip;
    public AudioClip fryingClip;
    public AudioClip grillingClip;
    public AudioClip finishClip;
    public AudioClip loseClip;
    public AudioClip winClip;

    public void PlayChopping()
    {
        audioSourceChopping.PlayOneShot(choppingClip);
    }

    public void PlayKneading()
    {
        audioSourceKneading.PlayOneShot(kneadingClip);
    }

    public void PlayFinish()
    {
        audioSourceFinish.PlayOneShot(finishClip);
    }

    public void StartFrying()
    {
        if (!audioSourceFrying.isPlaying)
        {
            audioSourceFrying.clip = fryingClip;
            audioSourceFrying.loop = true;
            audioSourceFrying.Play();
        }
    }

    public void StopFrying()
    {
        if (audioSourceFrying.isPlaying)
            audioSourceFrying.Stop();
    }

    public void StartGrilling()
    {
        if (!audioSourceGrilling.isPlaying)
        {
            audioSourceGrilling.clip = grillingClip;
            audioSourceGrilling.loop = true;
            audioSourceGrilling.Play();
        }
    }

    public void StopGrilling()
    {
        if (audioSourceGrilling.isPlaying)
            audioSourceGrilling.Stop();
    }

    public void PlayLose()
    {
        audioSourceLose.PlayOneShot(loseClip);
    }

    public void PlayWin()
    {
        audioSourceWin.PlayOneShot(winClip);
    }
}



