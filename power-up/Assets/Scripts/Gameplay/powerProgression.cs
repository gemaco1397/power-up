using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerProgression : MonoBehaviour
{
    [SerializeField] int powerNeeded;
    [SerializeField] Slider progress;
    float charge;
    [SerializeField] GameObject finishUI;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject Crosshair;

    bool active;

    private void Start()
    {
        PowerMaster.onPower += updatePower;
    }

    void updatePower(float power, float drain)
    {
        charge += power * Time.deltaTime;
        if(charge < powerNeeded)
        {
            progress.value = charge / powerNeeded;
        }
        else
        {
            finishGame();
        }
        
    }

    private void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                PlayerMoveController.i.EnableControlls();
                UI.SetActive(false);
                Crosshair.SetActive(true);
                active = false;
            }
        }   
        
        
    }

    public void openMenu()
    {
        UI.SetActive(true);
        active = true;
        PlayerMoveController.i.DisableControlls();
        Crosshair.SetActive(false);
    }

    void finishGame()
    {
        PlayerMoveController.i.DisableControlls();
        finishUI.SetActive(true);
        hud.SetActive(false);
    }

}
