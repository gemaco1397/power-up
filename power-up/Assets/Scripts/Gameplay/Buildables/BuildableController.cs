using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableController : MonoBehaviour
{
    [SerializeReference] float buildHeigtOffset;
    Rigidbody RB;
    [SerializeField] Collider[] buildCollider;
    [SerializeField] LayerMask groundLayers;
    Vector3 startPoint;
    bool clear = true;
    float buildPercantage;
    [SerializeField] float buildTime;
    [SerializeField] float powerDrain;
    bool building;

    PowerMaster PM;
    
    Coroutine buildPercentage;

    public delegate void buildFinished();
    public event buildFinished OnBuildFinished;

    public void StartBuilding()
    {
        RB = gameObject.AddComponent<Rigidbody>();
        RB.isKinematic = true;
        
        
        PM = PowerMaster.i;
    }


    

    public bool PlaceBuilding()
    {
        RaycastHit hit;
        startPoint = transform.localPosition;
        Physics.Raycast(transform.position, transform.up * -1, out hit, Mathf.Infinity, groundLayers);

        
        transform.position = hit.point + (transform.up * buildHeigtOffset); 

        if (clear)
        {
            FinishBuilding();
        }
        else
        {
            transform.localPosition = startPoint;
        }

        return clear;
    }

    private void FinishBuilding()
    {
        Destroy(RB);
        foreach(var collider in buildCollider)
        {
            collider.enabled = false;
        }
        building = true;
        buildPercentage = StartCoroutine(Builder());
    }

    public void SwitchBuilding()
    {
        if (building)
        {
            PowerMaster.OnPowerCollection -= drainPower;
            PowerMaster.OnBreakerTrip -= breaker;
            StopCoroutine(buildPercentage);
            building = false;
        }
        else
        {
            building = true;
            buildPercentage = StartCoroutine(Builder());
        }
        
    }

    private IEnumerator Builder()
    {
        PowerMaster.OnPowerCollection += drainPower;
        PowerMaster.OnBreakerTrip += breaker;
        var PPS = 1 / (buildTime / 100);
        while(buildPercantage < 100)
        {
            buildPercantage += PPS * Time.deltaTime;
            Debug.Log(buildPercantage);
            yield return null;
        }
        PowerMaster.OnPowerCollection -= drainPower;
        PowerMaster.OnBreakerTrip -= breaker;
        OnBuildFinished?.Invoke();
    }

    private void drainPower()
    {
        PM.CollectDrain(powerDrain);
    }

    private void breaker()
    {
        if (building)
        {
            SwitchBuilding();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        clear = false;
    }

    private void OnTriggerExit(Collider other)
    {
        clear = true;
    }

}
