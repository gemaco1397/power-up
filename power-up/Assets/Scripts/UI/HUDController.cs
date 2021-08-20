using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    [Header("wattage")]
    [SerializeField] private Text wattageText;
    // Start is called before the first frame update
    void Start()
    {
        PowerMaster.OnPowerChange += updateWattage;
    }

    private void updateWattage(float power)
    {
        power = Mathf.Floor(power);
        wattageText.text = power.ToString();
    }
}
