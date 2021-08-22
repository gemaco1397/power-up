using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CraftingProgressSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private TextMeshProUGUI textTag;

    private void Start()
    {
        slider = gameObject.GetComponentInChildren<Slider>();
        craftingController.onBuild += buildSlider;
    }

    private void buildSlider(float timer, float currenttime, string tag)
    {
        var percentage = 1 - timer / currenttime;
        slider.value = percentage;
        textTag.text = tag;
    }
}
