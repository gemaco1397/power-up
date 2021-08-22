using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMaster : MonoBehaviour
{
    public float power { get; private set; }
    public delegate void PowerChange(float power, float drain);
    public delegate void collectPower();
    public static event PowerChange OnPowerChange;
    public static event collectPower OnPowerCollection;
    public static event collectPower OnBreakerTrip;
    public static event PowerChange onPower;
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
        OnPowerChange?.Invoke(power, drain);
        
    }

    private void Update()
    {
        _power = 0;
        drain = 0;
        OnPowerCollection?.Invoke();
        if (_power < drain)
        {
            OnBreakerTrip?.Invoke();
        }
        else
        {
            _power -= drain;
        }
        //power for the ship calculation
        onPower?.Invoke(power, drain);
        if (power != _power)
        {

            OnPowerChange?.Invoke(_power, drain);
            power = _power;
        }
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
