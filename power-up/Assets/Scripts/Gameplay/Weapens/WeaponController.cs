using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : Upgradable
{
    [Header("gun balancing")]
    public float force;
    public bool auto;
    public float fireRate;
    public ParticleSystem particles;
    

    
}
