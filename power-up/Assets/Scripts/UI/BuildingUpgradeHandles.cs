using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpgradeHandles : MonoBehaviour
{
    [SerializeField] CustomButton[] buttons;

    private Upgradable upgradeTarget;
    [SerializeField]private InformationPanelController panel;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].startBottun(this, i);
        }
        
    }

    
    public void TriggerButton(int ID)
    {
        Debug.Log(ID);
        upgradeTarget.StartUpgrade(ID);

    }

    public void enableObject(Upgradable _upgradeTarget)
    {
        upgradeTarget = _upgradeTarget;
        gameObject.SetActive(true);
        PlayerMoveController.i.DisableControlls();
        for (int i = 0; i < buttons.Length; i++)
        {
            switch (i)
            {
                case 0:
                    buttons[i].updateTextTag(_upgradeTarget.buttonTagA);
                    break;
                case 1:
                    buttons[i].updateTextTag(_upgradeTarget.buttonTagB);
                    break;
            }
        }
        panel.Enable(_upgradeTarget);
    }

    

}
