using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformationPanelController : MonoBehaviour
{
    [Header("power")]
    [SerializeField] private TextMeshProUGUI powerTitle;
    [SerializeField] private TextMeshProUGUI powerValue;

    [Header("RPS")]
    [SerializeField] private TextMeshProUGUI RPSTitle;
    [SerializeField] private TextMeshProUGUI RPSValue;

    [Header("force")]
    [SerializeField] private TextMeshProUGUI forceTitle;
    [SerializeField] private TextMeshProUGUI forceValue;


    [Header("general")]
    [SerializeField] GameObject disableButton;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] TextMeshProUGUI title; 



    private Upgradable target;
    private BasicTurretController turretTarget;
    private WheelController generatorTarget;
    private upgradeType type;

    bool active;

    public void Enable(Upgradable _target)
    {
        target = _target;
        type = _target.type;
        switch (_target.type)
        {
            case upgradeType.generator:
                generatorText();
                break;
            case upgradeType.turret:
                turretText();
                break;
        }

        active = true;
    }


    void generatorText()
    {
        powerTitle.text = "generating:";
        generatorTarget = target.GetComponent<WheelController>();
        forceTitle.text = "weight";
        title.text = "generator settings";
        disableButton.SetActive(false);
    }

    void turretText()
    {
        turretTarget = target.GetComponent<BasicTurretController>();
        powerTitle.text = "consumption";
        forceTitle.text = "force";
        title.text = "turret settings";
        disableButton.SetActive(true);
        if (turretTarget.getActive())
        {
            buttonText.text = "turn off";
        }
        else
        {
            buttonText.text = "turn on";
        }
    }



    private void OnDisable()
    {
        active = false;
    }

    private void Update()
    {
        if (active)
        {
            switch (target.type)
            {
                case upgradeType.generator:
                    updateGeneratorText();
                    break;
                case upgradeType.turret:
                    updateTurretText();
                    break;
            }

            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                PlayerMoveController.i.EnableControlls();
                gameObject.SetActive(false);
            }
        }
    }

    public void SwitchTurret()
    {
        if (turretTarget.SwitchTurret())
        {
            buttonText.text = "turn off";
        }
        else
        {
            buttonText.text = "turn on";
        }
    }

    void updateGeneratorText()
    {
        powerValue.text = generatorTarget.GetPPS();
        RPSValue.text = generatorTarget.GetRPS();
        forceValue.text = generatorTarget.GetMass();
    }

    void updateTurretText()
    {
        powerValue.text = turretTarget.consumption();
        RPSValue.text = turretTarget.getRPS();
        forceValue.text = turretTarget.getForce();
    }

}
