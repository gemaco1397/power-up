using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    Transform cam;
    RaycastHit hit;
    [SerializeField] float force;
    [SerializeField] bool auto;
    [SerializeField] float fireRate;
    bool firing;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!auto)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if(!firing)
                {
                    firing = true;
                    StartCoroutine(autoFire());
                }
                
            }
            else
            {
                firing = false;
            }
        }
        
    }

    private IEnumerator autoFire()
    {
        while (firing)
        {
            
            Shoot();
            yield return new WaitForSeconds(1 / fireRate);
        }
    }

    void Shoot()
    {
        if(Physics.Raycast(cam.position, cam.forward, out hit))
        {
            if (hit.collider.CompareTag("Wheel"))
            {
                hit.collider.GetComponent<WheelController>().Hit(force);
            }
        }
    }
}
