using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurretController : WeaponController, IInteractable
{

    [SerializeField] string interactionTag =  "flip the turrets power switch";
    [SerializeField] float powerDrain;
    [SerializeField] Transform shootPoint;

    PowerMaster pm;

    bool active;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        pm = PowerMaster.i;
        
        PowerMaster.OnPowerCollection += power;
        PowerMaster.OnBreakerTrip += breaker;
    }

    private IEnumerator FireRate()
    {
        Shoot();
        yield return new WaitForSeconds(1 / fireRate);
        if (active)
        {
            StartCoroutine(FireRate());
        }
        
    }

    private void breaker()
    {
        Debug.Log("breaker");
        
        active = false;
    }

    private void power()
    {
        if (active)
        {
            pm.CollectDrain(powerDrain);
        }
    }

    private void Shoot()
    {
        if (Physics.Raycast(shootPoint.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Wheel"))
            {
                hit.collider.GetComponentInParent<WheelController>().Hit(force);
            }
        }
    }

    public string RecieveTag()
    {
        return interactionTag;
    }

    public void interact()
    {
        active = !active;
        if (active)
        {
            StartCoroutine(FireRate());
        }
        Debug.Log(active);
    }
}
