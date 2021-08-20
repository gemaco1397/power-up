using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMaster : MonoBehaviour
{
    float power;
    public delegate void PowerChange(float power, bool lower);
    public delegate void collectPower();
    public static event PowerChange OnPowerChange;
    public static event collectPower OnPowerCollection;
    float _power;
    float drain;
    


    #region singleton
    public static PowerMaster i;
    private void Awake()
    {
        i = this;
    }

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        OnPowerChange?.Invoke(power, false);
        StartCoroutine(powerCollection());
    }

    

    private IEnumerator powerCollection()
    {
        _power = 0;
        OnPowerCollection?.Invoke();
        if(power < drain)
        {
            //trip breaker
        }
        if (power != _power)
        {
            //power has changed, inform all things relying on this, and add the true/false result of if the current power is lower than the previous power
            OnPowerChange(_power, power > _power);
            power = _power;
        }
        yield return new WaitForSeconds(0.1f);
    }



    public void CollectDrain(float powerToDrain)
    {
        drain += powerToDrain;
    }

    public void AddPower(float powerToAdd)
    {
        _power += powerToAdd;
    }
    
}
