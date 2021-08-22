using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        PlayerMoveController.i.DisableControlls();
    }

    public void start()
    {
        Time.timeScale = 1;
        PlayerMoveController.i.EnableControlls();
    }
}
