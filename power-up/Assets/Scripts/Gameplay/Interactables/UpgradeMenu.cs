using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour, IInteractable
{

    [SerializeField] string returnTag;

    public void interact()
    {
        
    }

    public string RecieveTag()
    {
               
        return returnTag;
    }

    
}
