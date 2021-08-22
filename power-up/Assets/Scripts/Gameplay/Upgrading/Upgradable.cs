using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgradable : MonoBehaviour
{
    [HideInInspector]
    public int upgradeALevel;
    [HideInInspector]
    public int upgradeBLevel;
    [Header("upgrade balancing")]
    public float upgradeADuration;
    public float upgradeBDuration;
    public float UpgradeADrain;
    public float UpgradeBDrain;

    [HideInInspector]
    public PowerMaster PM;


    [Header("Unity")]
    public upgradeType type;
    public string buttonTagA;
    public string buttonTagB;
    [HideInInspector]
    public GameObject upgradableUI;
    [HideInInspector]
    public BuildingUpgradeHandles handler;
    bool upgrading;
    [HideInInspector]
    public float currentDrain;
    public delegate void UpgradeDuration(float duration, float totalDuration);

    public event UpgradeDuration onUpgrade;

    public Coroutine upgradeTimer;
    public virtual void Start()
    {
        PM = PowerMaster.i;
        upgradableUI = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<UIContainer>().upgradeMenu;
        handler = upgradableUI.GetComponent<BuildingUpgradeHandles>();
        
    }


    public virtual void StartUpgrade(int ID)
    {
        switch (ID)
        {
            case 0:
                upgradeTimer = StartCoroutine(UpgradeTimer(upgradeADuration, ID));
                currentDrain = UpgradeADrain;
                break;
            case 1:
                upgradeTimer = StartCoroutine(UpgradeTimer(upgradeBDuration, ID));
                currentDrain = UpgradeBDrain;
                break;
        }
    }

    public virtual IEnumerator UpgradeTimer(float duration, int ID)
    {
        var timer = duration;
        upgrading = true;
        PowerMaster.OnPowerCollection += drainPower;
        PowerMaster.OnBreakerTrip += breaker;
        while(timer > 0)
        {
            
            timer -= Time.deltaTime;
            onUpgrade?.Invoke(timer, duration);
            yield return null;
        }
        PowerMaster.OnPowerCollection -= drainPower;
        PowerMaster.OnBreakerTrip -= breaker;
        upgrading = false;
        switch (ID)
        {
            case 0:
                IncreaseLevelA();
                break;
            case 1:
                IncreaseLevelB();
                break;  
        }
           

    }

    public virtual void IncreaseLevelA()
    {
        
    }

    public virtual void IncreaseLevelB()
    {

    }
    

    public void drainPower()
    {
        PM.CollectDrain(currentDrain * Time.deltaTime);
    }

    public virtual void breaker()
    {
        if (upgrading)
        {
            StopCoroutine(upgradeTimer);
            upgrading = false;
        }
        
    }

    

}

public enum upgradeType
{
    generator, turret
}

