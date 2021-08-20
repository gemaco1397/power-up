using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    
    public float force;
    public bool auto;
    public float fireRate;

    public virtual void UpgradeWeapon()
    {
        //TODO weaponUpgrades
    }

    
}
