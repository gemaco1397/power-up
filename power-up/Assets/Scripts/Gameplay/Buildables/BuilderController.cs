using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuilderController : MonoBehaviour
{

    [SerializeField] buildable[] buildables;
    
    InventoryController inv;
    bool available;
    BuildingTypes buildtype;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI pistolinv;
    [SerializeField] TextMeshProUGUI ARInv, lightgenInv, heavyGenInv;

   
    private void Start()
    {
        inv = InventoryController.i;
        craftingController.onFinish += updateUI;
        updateUI();

    }

    public void startBuild(int ID)
    {

        switch (ID)
        {
            case 0:
                if (inv.pistolInventory > 0)
                {
                    available = true;
                    buildtype = BuildingTypes.pistol;
                }

                break;
            case 1:
                if (inv.ARInventory > 0)
                {
                    available = true;
                    buildtype = BuildingTypes.AR;
                }
                break;
            case 2:
                if (inv.lightGenInventory > 0)
                {
                    available = true;
                    buildtype = BuildingTypes.lightgen;
                }
                break;
            case 3:
                if (inv.heavyGenInventory > 0)
                {
                    available = true;
                    buildtype = BuildingTypes.heavygen;
                }
                break;
        }
        
        if(available)
        {
            PlayerBuildController.i.RecieveBuilding(buildables[ID]);
            available = false;
            inv.removeInv(buildtype);
            updateUI();
            gameObject.SetActive(false);
        }
        
        

        
    }

    void updateUI()
    {
        var maintag = "available: ";
        pistolinv.text = maintag + inv.pistolInventory.ToString();
        ARInv.text = maintag + inv.ARInventory.ToString();
        lightgenInv.text = maintag + inv.lightGenInventory.ToString();
        heavyGenInv.text = maintag + inv.heavyGenInventory.ToString();
    }
}
