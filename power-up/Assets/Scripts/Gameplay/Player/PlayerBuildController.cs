using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildController : MonoBehaviour
{

    
    
    bool building;
    bool UIOpen;
    GameObject buildingObject;
    BuildableController BC;

    [SerializeField] GameObject buildUI;

    #region singleton
    public static PlayerBuildController i;
    private void Awake()
    {
        i = this;
    }
    #endregion

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            buildUI.SetActive(true);
            PlayerMoveController.i.DisableControlls();
            UIOpen = true;
        }
        if (building || UIOpen)
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                if (!UIOpen)
                {
                    Destroy(buildingObject);
                    building = false;
                }
                else
                {
                    PlayerMoveController.i.EnableControlls();
                    buildUI.SetActive(false);
                    UIOpen = false;
                }
                
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    if (!UIOpen)
                    {
                        if (BC.PlaceBuilding())
                        {
                            buildingObject.transform.parent = null;
                            building = false;
                        }
                        else
                        {
                            Debug.Log(false);
                        }
                    }
                    
                    
                }
            }
        }

    }

    public void RecieveBuilding(buildable _building)
    {
        if (!building)
        {
            spawnBuilding(_building);
        }
        else
        {
            Destroy(buildingObject);
            spawnBuilding(_building);
        }
        PlayerMoveController.i.EnableControlls();
        UIOpen = false;
    }

    private void spawnBuilding(buildable _building)
    {
        var buildpoint = transform.position + (transform.forward * _building.buildOffset.z + transform.right * _building.buildOffset.x);
        buildingObject = Instantiate(_building.objectToBuild, buildpoint, transform.rotation, transform);
        BC = buildingObject.GetComponent<BuildableController>();
        BC.StartBuilding();
        building = true;
    }
}



[System.Serializable]
public class buildable 
{
    public GameObject objectToBuild;
    public Vector3 buildOffset;
}
