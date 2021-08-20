using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookController : MonoBehaviour
{
    Vector2 mouse;
    [SerializeField] float sensitivity = 10;
    Vector3 targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            mouse.x = Input.GetAxis("Mouse X");
            mouse.y = Input.GetAxis("Mouse Y");
            mouse *= sensitivity * Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        

        if(mouse.x != 0)
        {
            rotatePlayer();
        }
        if(mouse.y != 0)
        {
            
            RotateEyes();
        }

    }

    void rotatePlayer()
    {
        transform.parent.rotation = Quaternion.Euler(transform.parent.eulerAngles + (Vector3.up * mouse.x));
    }

    void RotateEyes()
    {
        targetRotation.x -= mouse.y;
        targetRotation.x = Mathf.Clamp(targetRotation.x, -90, 90);
        transform.localRotation = Quaternion.Euler(targetRotation);
    }
}
