using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioManager audioManager;

    void OnCollisionEnter(Collision collision)
    {

        // store the tags first of the two colliding objects.
        // then we compare the tags of the two objects.
        // then based on that we can play an audio clip based on the tags.

        string Tag = gameObject.tag;
        string otherTag = collision.gameObject.tag;

        if ((Tag == "Potato" && otherTag == "Knife") || (Tag == "Knife" && otherTag == "Potato"))
        {
            audioManager.PlayChopping();
        }

        if ((Tag == "Dough" && otherTag == "Pin") || (Tag == "Pin" && otherTag == "Dough"))
        {
            audioManager.PlayKneading();
        }


        // Im not sure how to implement the frying and grilling audio 
        // whenever the cut potato is in the fryer or the uncooked meat is on the grill. it stars the audio but
        // once it destorys the cut potato or uncooked meat, it should stop the audio.
        // but it doesnt and I dont know how to implement it.

        //    if ((Tag == "UncookedMeat" && otherTag == "Grill") || (Tag == "Grill" && otherTag == "UncookedMeat"))
        //    {
        //        audioManager.StartGrilling();
        //    }

        //    if ((Tag == "CutPotato" && otherTag == "Frier") || (Tag == "Frier" && otherTag == "CutPotato"))
        //    {
        //        audioManager.StartFrying();
        //    }
        //}

        //void OnCollisionExit(Collision collision)
        //{

        //    string Tag = gameObject.tag;
        //    string otherTag = collision.gameObject.tag;

        //    if ((Tag == "UncookedMeat" && otherTag == "Grill") || (Tag == "Grill" && otherTag == "UncookedMeat"))
        //    {
        //        audioManager.StopGrilling();
        //    }

        //    if ((Tag == "CutPotato" && otherTag == "Frier") || (Tag == "Frier" && otherTag == "CutPotato"))
        //    {
        //        audioManager.StopFrying();
        //    }
        //}
    }
}