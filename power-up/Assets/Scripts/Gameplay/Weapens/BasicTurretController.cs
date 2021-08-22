using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurretController : WeaponController, IInteractable
{
    [Header("Turret unity stuff")]
    [SerializeField] string interactionTag =  "flip the turrets power switch";
    [SerializeField] float powerDrain;
    [SerializeField] Transform shootPoint;

    PowerMaster pm;
    bool build;
    bool active;
    RaycastHit hit;
    BuildableController BC;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        pm = PowerMaster.i;
        BC = GetComponent<BuildableController>();
        PowerMaster.OnPowerCollection += power;
        PowerMaster.OnBreakerTrip += breaker;
        BC.OnBuildFinished += EnableTurret;
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

    private void EnableTurret()
    {
        build = true;
    }

    public override void breaker()
    {
        base.breaker();
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
        particles.Play();
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
        if(build)
        {
            handler.enableObject(this);
        }
        else
        {
            BC.SwitchBuilding();
        }
    }

    public bool SwitchTurret()
    {
        active = !active;
        if (active)
        {
            StartCoroutine(FireRate());
        }

        return active;
    }
    
    public bool getActive()
    {
        return active;
    }

    public string consumption()
    {
        return powerDrain.ToString();
    }

    public string getRPS()
    {
        return fireRate.ToString();
    }

    public string getForce()
    {
        return force.ToString();
    }

}
