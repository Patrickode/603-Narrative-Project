using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scripts allows player to interact with an game object in the item layer.
/// 
/// To use it you need to specify two variables:
/// 
/// Time: Time used for camera tweening. Note that the return takes half the time.
/// Target Transform: The transform where you want the player to inspect. Create a child empty object inside the item and assign it in the inspector would be a good way to do this.
/// 
/// </summary>
public class Item_ZoomIn : Interactable
{
    [Header("Edit Properties Below")]
    public float time;
    public Transform targetTransform;
    public override void Interact()
    {
        DisablePlayerMovement();
        LeanTween.move(PlayerCam.instance.cam.gameObject, targetTransform.position, time);
        LeanTween.rotate(PlayerCam.instance.cam.gameObject, targetTransform.rotation.eulerAngles, time);
        Invoke("SetFlagToTrue", time + 0.2f);
    }

    private void SetFlagToTrue()
    {
        isInteracting = true;
    }

    private void SetFlagToFalse()
    {
        isInteracting = false;
    }

    public void Return()
    {
        LeanTween.moveLocal(PlayerCam.instance.cam.gameObject, new Vector3(0, 0.5f, 0), time / 2f);
        LeanTween.rotateLocal(PlayerCam.instance.cam.gameObject, new Vector3(PlayerLook.instance.xRotation, 0, 0), time / 2f);
        Invoke("SetFlagToFalse", time / 2f + 0.2f);
        Invoke("EnablePlayerMovement", time / 2f + 0.2f);
    }
    public void DisablePlayerMovement()
    {
        PlayerMovement.instance.isDisabled = true;
        PlayerLook.instance.isDisabled = true;
    }

    public void EnablePlayerMovement()
    {
        PlayerMovement.instance.isDisabled = false;
        PlayerLook.instance.isDisabled = false;
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0) && isInteracting)
        {
            Return();
        }
    }

}
