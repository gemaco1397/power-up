using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{

    [Header("wattage")]
    [SerializeField] private TextMeshProUGUI wattageText;
    [SerializeField] private TextMeshProUGUI drainText;
    // Start is called before the first frame update
    void Awake()
    {
        PowerMaster.OnPowerChange += updateWattage;
    }

    private void updateWattage(float power, float drain)
    {
        power += drain;
        power = Mathf.Floor(power);
        drain = Mathf.Floor(drain);
        wattageText.text = "generating: " + power.ToString();
        drainText.text = "draining: " + drain.ToString();
    }
}
