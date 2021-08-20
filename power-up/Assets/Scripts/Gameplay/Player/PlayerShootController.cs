using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    Transform cam;
    RaycastHit hit;
    [SerializeField] WeaponController selectedWeapon;
    List<GameObject> availableWeapons = new List<GameObject>();
    [SerializeField] Transform weaponPosition;
    
    bool firing;


    private void Start()
    {
        cam = Camera.main.transform;
        selectedWeapon.transform.parent = weaponPosition;
        selectedWeapon.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        FiringManagement();
        
        
    }


    private void FiringManagement()
    {
        if (!selectedWeapon.auto)
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
                if (!firing)
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
            yield return new WaitForSeconds(1 / selectedWeapon.fireRate);
        }
    }

    public void Shoot()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit))
        {
            if (hit.collider.CompareTag("Wheel"))
            {
                
                hit.collider.GetComponentInParent<WheelController>().Hit(selectedWeapon.force);
            }
        }
    }
}
