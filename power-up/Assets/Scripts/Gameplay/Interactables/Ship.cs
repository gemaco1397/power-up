using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IInteractable
{
    powerProgression PG;
    void Start()
    {
        PG = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<powerProgression>();
    }

    public void interact()
    {
        PG.openMenu();


    }

    public string RecieveTag()
    {
        return "";
    }

   
}
