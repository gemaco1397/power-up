using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerWeaponController : MonoBehaviour
{
    [Header("gun balancing")]
    public float force;
    public bool auto;
    public float fireRate;
    public ParticleSystem particles;
}
