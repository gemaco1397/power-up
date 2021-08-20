using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{

    [SerializeField] float maxVelocity;
    [SerializeField] float resistance;
    [SerializeField] float powerPerRotation;
    [SerializeField] float mass;
    float velocity;
    Vector3 rotation;
    float rps;
    PowerMaster PM;

    private void Start()
    {
        PM = PowerMaster.i;
        PowerMaster.OnPowerCollection += collectPower;


    }

    private void Update()
    {
        rotation.z += velocity * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(rotation);
        rps = velocity / 360;
        if(velocity > 0)
        {
            velocity -= resistance/mass * Time.deltaTime;
            if(velocity < 0)
            {
                velocity = 0;
            }
        }
        
    }

    private void collectPower()
    {
        PM.AddPower(rps * powerPerRotation);
    }

    public void Hit(float force)
    {
        
        
        velocity += force/mass;
        if (velocity > maxVelocity)
            velocity = maxVelocity;




    }


}
