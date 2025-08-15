using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource ChoppingAudio;
    public AudioSource KneadingAudio;
    public AudioSource FryingAudio;
    public AudioSource GrillingAudio;

    public AudioClip choppingClip;
    public AudioClip kneadingClip;
    public AudioClip fryingClip;
    public AudioClip grillingClip;

    public void PlayChopping()
    {
            ChoppingAudio.PlayOneShot(choppingClip);
    }

    public void PlayKneading()
    {
            KneadingAudio.PlayOneShot(kneadingClip);
    }



    // Im not sure how to implement the frying and grilling audio 
    // whenever the cut potato is in the fryer or the uncooked meat is on the grill.
    //once it destorys the cut potato or uncooked meat, it should stop the audio.
    // but it doesnt and I dont know how to implement it.

    //public void StartFrying()
    //{
    //    FryingAudio.clip = fryingClip;
    //    FryingAudio.Play();
    //}

    //public void StopFrying()
    //{
    //    FryingAudio.Stop();
    //}

    //public void StartGrilling()
    //{
    //    GrillingAudio.clip = grillingClip;
    //    GrillingAudio.Play();
    //}

    //public void StopGrilling()
    //{
    //    GrillingAudio.Stop();
    //}
}



