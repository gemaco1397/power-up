using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainer : MonoBehaviour
{
    public GameObject weaponUpgrades;
    public GameObject buildObject;
    public GameObject craftMenu;
    public GameObject upgradeMenu;
    

    #region singleton
    UIContainer i;
    private void Awake()
    {
        i = this;
    }
    #endregion
}
