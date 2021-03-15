using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float range;
    public LayerMask itemMask;

    void Update()
    {
        if (!PlayerLook.instance.isDisabled)
        {
            PlayerCam cachedPCamInstance = PlayerCam.instance;
            if (cachedPCamInstance && cachedPCamInstance.cam && Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(
                    cachedPCamInstance.cam.transform.position,
                    cachedPCamInstance.cam.transform.forward,
                    out RaycastHit hit,
                    range,
                    itemMask
                ))
                {
                    if (hit.transform.gameObject.GetComponent<Interactable>())
                    {
                        hit.transform.gameObject.GetComponent<Interactable>().Interact();
                    }
                }
            }

            //if (Physics.Raycast(
            //        cachedPCamInstance.cam.transform.position,
            //        cachedPCamInstance.cam.transform.forward,
            //        out RaycastHit hit2,
            //        range,
            //        itemMask
            //    ))
            //{
            //    hit2.transform.gameObject.GetComponent<Renderer>().material.SetFloat("_SecondOutlineWidth", hit2.transform.gameObject.GetComponent<Interactable>().hoveringOutlineWidth);
            //}
        }
    }
}
