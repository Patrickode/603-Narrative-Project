using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float range;
    public LayerMask itemMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerLook.instance.isDisabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if(Physics.Raycast(PlayerCam.instance.cam.transform.position, PlayerCam.instance.cam.transform.forward, out hit, range, itemMask))
                {
                    if (hit.transform.gameObject.GetComponent<Interactable>())
                    {
                        hit.transform.gameObject.GetComponent<Interactable>().Interact();
                    }
                }
            }
        }
    }
}
