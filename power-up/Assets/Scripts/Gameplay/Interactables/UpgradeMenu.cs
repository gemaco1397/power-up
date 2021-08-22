using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeMenu : MonoBehaviour, IInteractable
{

    [SerializeField] string returnTag;
    
    public UnityEvent openEvent;

    public void interact()
    {
        if(openEvent != null)
        {
            openEvent.Invoke();
        }
    }

    public string RecieveTag()
    {
               
        return returnTag;
    }

    
}
