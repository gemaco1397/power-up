using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMaster : MonoBehaviour
{
    [SerializeField] float power;
    public delegate void PowerChange(float power);
    public delegate void collectPower();
    public static event PowerChange OnPowerChange;
    public static event collectPower OnPowerCollection;
    public static event collectPower OnBreakerTrip;
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
        OnPowerChange?.Invoke(power);
        StartCoroutine(powerCollection());
    }

    

    private IEnumerator powerCollection()
    {
        _power = 0;
        drain = 0;
        OnPowerCollection?.Invoke();
        if(_power < drain)
        {
            OnBreakerTrip?.Invoke();
        }
        else
        {
            _power -= drain;
        }
        if (power != _power)
        {
            
            OnPowerChange?.Invoke(_power);
            power = _power;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(powerCollection());
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
