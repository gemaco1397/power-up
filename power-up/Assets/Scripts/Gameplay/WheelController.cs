using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : Upgradable, IInteractable
{

    [SerializeField] float maxVelocity;
    [SerializeField] float resistance;
    [SerializeField] float powerPerRotation;
    [SerializeField] float mass;
    [SerializeField] float drag;
    [SerializeField] Transform wheel;
    float airRisistance;
    float velocity;
    Vector3 rotation;
    float rps;
    BuildableController BC;
    [SerializeField] bool finished;
    [SerializeField] bool initial;

    public override void Start()
    {
        base.Start();
        PM = PowerMaster.i;
        if (!finished)
        {
            BC = GetComponent<BuildableController>();

            BC.OnBuildFinished += onBuildingFinished;
        }
        else
        {
            PowerMaster.OnPowerCollection += collectPower;
        }
        
    }
  

    private void Update()
    {
        rotation.z += velocity * Time.deltaTime;
        wheel.localRotation = Quaternion.Euler(rotation);
        
        rps = velocity / 360;
        if(velocity > 0)
        {
            velocity -= resistance/mass * Time.deltaTime;
            if(velocity < 0)
            {
                velocity = 0;
            }
        }
        //airRisistance = 0.5f * 1 * (velocity * velocity) * drag;
        //velocity -= airRisistance * Time.deltaTime;

    }

    private void collectPower()
    {
        PM.AddPower(rps * powerPerRotation);
    }

    public void Hit(float force)
    {

        if (finished)
        {
            velocity += force / mass;
            if (velocity > maxVelocity)
                velocity = maxVelocity;
        }
        
    }

    public string GetMass()
    {
        return mass.ToString();
    }

    public string GetRPS()
    {
        return rps.ToString("F2");
    }

    public string GetPPS()
    {
        return Mathf.Floor(rps * powerPerRotation).ToString();
    }


    private void onBuildingFinished()
    {
        finished = true;
        PowerMaster.OnPowerCollection += collectPower;
    }

    public string RecieveTag()
    {
        if (!finished)
        {
            return "pauseConstruction";
        }
        else
        {
            if (!initial)
            {
                return "Open generator Controlls";
            }
            else
            {
                return "";
            }
                   
        }
        
    }

    public void interact()
    {
        if (!finished)
        {
            BC.SwitchBuilding();
        }
        else
        {
            if (!initial)
            {
                
                handler.enableObject(this);
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
