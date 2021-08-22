using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public int pistolInventory { get; private set; }
    public int ARInventory { get; private set; }
    public int lightGenInventory { get; private set; }
    public int heavyGenInventory { get; private set; }

    #region singleton
    public static InventoryController i;
    private void Awake()
    {
        i = this;
    }
    #endregion
    public void AddInv(BuildingTypes buildType)
    {
        switch (buildType)
        {
            case BuildingTypes.pistol:
                pistolInventory++;
                break;
            case BuildingTypes.AR:
                ARInventory++;
                break;
            case BuildingTypes.lightgen:
                lightGenInventory++;
                break;
            case BuildingTypes.heavygen:
                heavyGenInventory++;
                break;
        }
    }
    public void removeInv(BuildingTypes buildType)
    {
        switch (buildType)
        {
            case BuildingTypes.pistol:
                pistolInventory--;
                break;
            case BuildingTypes.AR:
                ARInventory--;
                break;
            case BuildingTypes.lightgen:
                lightGenInventory--;
                break;
            case BuildingTypes.heavygen:
                heavyGenInventory--;
                break;
        }
    }
    
}

public enum BuildingTypes {pistol, AR, lightgen, heavygen}