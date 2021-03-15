using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scripts allows player to interact with an game object in the item layer.
/// 
/// To use it you need to specify a few variables:
/// 
/// Time: Time used for camera tweening. Note that the return takes half the time.
/// Delay: Delay the time of unlocking controls and interactions. (Increase this value if you want to force player to listen to voice lines)
/// Target Transform: The transform that the camera will move to/align with. Create an empty child inside the item and assign it in the inspector would be a good way to do this.
/// Clip: If this field is not null, then when the player zooms in, this audio clip will play.
/// 
/// </summary>
public class Item_ZoomIn : Interactable
{
    [Header("Edit Properties Below")]
    [SerializeField]
    [Range(0.5f, 5f)] public float zoomTime = 0.5f;
    [Range(0f, 20f)] public float delay = 0f;
    public Transform targetTransform;

    [SerializeField] private AudioClip clip = null;
    [Tooltip("If true, ignores the delay field, and acts as if delay = clip's length.")]
    [SerializeField] private bool delayEqualsClipLength = false;
    private bool delayFinished;

    public static Action<bool> DelayStartedOrEnded;


    public override void Interact()
    {
        DisablePlayerMovement();
        LeanTween.move(PlayerCam.instance.cam.gameObject, targetTransform.position, zoomTime);
        LeanTween.rotate(PlayerCam.instance.cam.gameObject, targetTransform.rotation.eulerAngles, zoomTime);
        Invoke("SetFlagToTrue", zoomTime);
        StartCoroutine(DelayTimer());
    }

    private void SetFlagToTrue()
    {
        isInteracting = true;

        if (clip)
        {
            AudioManager.playAudioAtTransform(PlayerCam.instance.cam.transform, clip, 1);
        }
    }

    private void SetFlagToFalse()
    {
        isInteracting = false;
    }

    public void Return()
    {
        LeanTween.moveLocal(PlayerCam.instance.cam.gameObject, new Vector3(0, 0.5f, 0), zoomTime / 2f);
        LeanTween.rotateLocal(PlayerCam.instance.cam.gameObject, new Vector3(PlayerLook.instance.xRotation, 0, 0), zoomTime / 2f);
        Invoke("SetFlagToFalse", zoomTime / 2f);
        Invoke("EnablePlayerMovement", zoomTime / 2f);
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
        if (delayEqualsClipLength) { delay = clip.length; }

        if (delayFinished && Input.GetMouseButtonDown(0) && isInteracting)
        {
            Return();
        }
    }

    private IEnumerator DelayTimer()
    {
        DelayStartedOrEnded?.Invoke(true);
        delayFinished = false;
        yield return new WaitForSeconds(delay);
        delayFinished = true;
        DelayStartedOrEnded?.Invoke(false);
    }
}
