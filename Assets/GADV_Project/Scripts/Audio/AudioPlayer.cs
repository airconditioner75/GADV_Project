using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioManager audioManager;

    void Awake()
    {
        if (!audioManager) audioManager = FindObjectOfType<AudioManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!audioManager) return;

        string a = gameObject.tag;
        string b = collision.gameObject.tag;

        // Chop
        if ((a == "Potato" && b == "Knife") || (a == "Knife" && b == "Potato"))
            audioManager.PlayChopping();

        // Knead
        if ((a == "Dough" && b == "Pin") || (a == "Pin" && b == "Dough"))
            audioManager.PlayKneading();

        // Grill start
        if ((a == "UncookedMeat" && b == "Grill") || (a == "Grill" && b == "UncookedMeat"))
            audioManager.StartGrilling();

        // Fry start
        if ((a == "CutPotato" && b == "Fryer") || (a == "Fryer" && b == "CutPotato"))
            audioManager.StartFrying();
    }

    void OnCollisionExit(Collision collision)
    {
        if (!audioManager) return;

        string a = gameObject.tag;
        string b = collision.gameObject.tag;

        // Grill stop
        if ((a == "UncookedMeat" && b == "Grill") || (a == "Grill" && b == "UncookedMeat"))
            audioManager.StopGrilling();

        // Fry stop
        if ((a == "CutPotato" && b == "Fryer") || (a == "Fryer" && b == "CutPotato"))
            audioManager.StopFrying();
    }
}