using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public GameObject Player;
    //public GameObject Item;

    void OnMouseDown()
    {
        Debug.Log("Mpuse down on " + gameObject.name);
    }
}
