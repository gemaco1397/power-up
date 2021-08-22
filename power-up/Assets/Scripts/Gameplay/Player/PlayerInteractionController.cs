using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    RaycastHit hit;
    public delegate void intaraction(string tag, bool active);
    public static event intaraction OnInteractionEnter;
    private Transform cam;
    private IInteractable targeted;
    private IInteractable _targeted;
    [SerializeField] float interactionRange;
    private bool interactable;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(Physics.Raycast(cam.position, cam.forward, out hit, interactionRange))
        {
            targeted = hit.collider.GetComponent<IInteractable>();
            if(targeted == null)
            {
                targeted = hit.collider.GetComponentInParent<IInteractable>();
            }
        }
        else
        {
            targeted = null;
        }

        if(targeted != _targeted)
        {
            _targeted = targeted;
            if(targeted != null)
            {
                interactable = true;
                targeted.RecieveTag();
                OnInteractionEnter?.Invoke(targeted.RecieveTag(), true);
            }
            else
            {
                interactable = false;
                OnInteractionEnter?.Invoke("", false);
            }
        }

        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                targeted.interact();
            }
        }
    }


}
