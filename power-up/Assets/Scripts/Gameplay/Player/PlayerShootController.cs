using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    Transform cam;
    RaycastHit hit;
    [SerializeField] PlayerWeaponController selectedWeapon;
    [SerializeField] List<PlayerWeaponController> availableWeapons = new List<PlayerWeaponController>();
    [SerializeField] Transform weaponPosition;
    [SerializeField] Camera fpscam;

    int index;
    bool firing;
    PlayerMoveController PMC;
    float scroll;
    bool aliveLastFrame;
    private void Start()
    {
        cam = Camera.main.transform;
        selectedWeapon.transform.parent = weaponPosition;
        selectedWeapon.transform.localPosition = Vector3.zero;
        PMC = PlayerMoveController.i;
    }

    // Update is called once per frame
    void Update()
    {
        if (aliveLastFrame)
        {
            FiringManagement();
            scroll = Input.mouseScrollDelta.y;
            
            if(scroll > 0)
            {
                index++;
                if (index >= availableWeapons.Count)
                    index = 0;
                switchWeapon();
            }
            else
            {
                if(scroll < 0)
                {
                    index--;
                    if (index < 0)
                        index = availableWeapons.Count - 1;
                    switchWeapon();
                    
                }
                
            }
        }
        
        if(aliveLastFrame != PMC.alive)
        {
            StartCoroutine(firedelay());
        }
        
    }


    //waits just a second before alowing the player to fire, to prevent the gun from firing when clicking an exit button or the start button
    private IEnumerator firedelay()
    {
        yield return new WaitForSeconds(0.1f);
        aliveLastFrame = PMC.alive;
    }


    void switchWeapon()
    {
        var currentTansform = selectedWeapon.transform;
        currentTansform.parent = null;
        currentTansform.position = Vector3.down * 3;
        selectedWeapon = availableWeapons[index];
        selectedWeapon.transform.parent = weaponPosition;
        selectedWeapon.transform.localPosition = Vector3.zero;
        selectedWeapon.transform.localRotation = Quaternion.identity;

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
        selectedWeapon.particles.Play();
        if (Physics.Raycast(cam.position, cam.forward, out hit))
        {
            if (hit.collider.CompareTag("Wheel"))
            {
                
                hit.collider.GetComponentInParent<WheelController>().Hit(selectedWeapon.force);
            }
        }
    }
}
