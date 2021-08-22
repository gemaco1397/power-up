using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftingController : MonoBehaviour
{
    [Header("pistol turret")]
    [SerializeField] private float pistolCraftDuration;
    [SerializeField] private float pistolCraftDrain;
    [Header("AR turret")]
    [SerializeField] private float ARCraftDuration;
    [SerializeField] private float ARCraftDrain;
    [Header("light generator")]
    [SerializeField] private float lightGenDuration;
    [SerializeField] private float lightGentDrain;
    [Header("heavy generator")]
    [SerializeField] private float heavyGenDuration;
    [SerializeField] private float heavyGentDrain;

    [SerializeField] private GameObject UI;


    [SerializeField] private GameObject Buttons, TurretUI, generatorUI, craftingUI;

    private BuildingTypes typeBuild;
    private float currentDrain;
    private float currentDuration;
    private Coroutine coroutine;
    public bool crafting;
    private string currentTag;
    bool UIOpen;

    private PowerMaster PM;

    public delegate void buildEvent(float timer, float totalTime, string tag);
    public delegate void finished();

    public static event buildEvent onBuild;
    public static event finished onFinish;


    #region singleton
    craftingController i;
    private void Awake()
    {
        i = this;
    }
    #endregion

    private void Start()
    {
        PM = PowerMaster.i;
        
    }

    private void Update()
    {
        if (UIOpen)
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                closeUI();
            }
        }
    }

    public void craftButton(int ID)
    {
        StartCraft((BuildingTypes)ID);
        
    }

    public void openUI()
    {
        UIOpen = true;
        UI.SetActive(true);
        PlayerMoveController.i.DisableControlls();
    }

    public void closeUI()
    {
        UIOpen = false;
        UI.SetActive(false);
        PlayerMoveController.i.EnableControlls();
    }

    private void StartCraft(BuildingTypes buildingTypes)
    {
        typeBuild = buildingTypes;
        startBuilding();
    }
    private void startBuilding()
    {
        switch (typeBuild)
        {
            case BuildingTypes.pistol:
                currentDrain = pistolCraftDrain;
                currentDuration = pistolCraftDuration;
                currentTag = "Pistol turret";
                break;
            case BuildingTypes.AR:
                currentDrain = ARCraftDrain;
                currentDuration = ARCraftDuration;
                currentTag = "assault rifle turret";
                break;
            case BuildingTypes.lightgen:
                currentDrain = lightGentDrain;
                currentDuration = lightGenDuration;
                currentTag = "light generator";
                break;
            case BuildingTypes.heavygen:
                currentDrain = heavyGentDrain;
                currentDuration = heavyGenDuration;
                currentTag = "heavy generator";
                break;
        }
        if(PM.power >= currentDrain)
        {
            EnableCraftView();
            coroutine = StartCoroutine(startbuild(currentDuration));
        }
        
    }

    private IEnumerator startbuild(float timer)
    {
        PowerMaster.OnPowerCollection += drain;
        PowerMaster.OnBreakerTrip += Breaker;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            onBuild?.Invoke(timer, currentDuration, currentTag);
            Debug.Log(timer);
            yield return null;
        }

        EndBuild();
        InventoryController.i.AddInv(typeBuild);
        onFinish?.Invoke();
    }
    private void drain()
    {
        PM.CollectDrain(currentDrain);
    }
    private void EndBuild()
    {
        PowerMaster.OnPowerCollection -= drain;
        PowerMaster.OnBreakerTrip -= Breaker;
        DisableCraftView();
    }

    private void Breaker()
    {
        StopCoroutine(coroutine);
        EndBuild();
    }

    void EnableCraftView()
    {
        Buttons.SetActive(false);
        TurretUI.SetActive(false);
        generatorUI.SetActive(false);
        craftingUI.SetActive(true);
    }
    void DisableCraftView()
    {
        Buttons.SetActive(true);
        TurretUI.SetActive(true);
        craftingUI.SetActive(false);
    }
}
