using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericUIControlls : MonoBehaviour
{
    public void StartGame()
    {

    }

    public void CloseObject(GameObject _object)
    {
        _object.SetActive(false);
    }
    

    public void OpenObject(GameObject _object)
    {
        _object.SetActive(true);
    }
}
