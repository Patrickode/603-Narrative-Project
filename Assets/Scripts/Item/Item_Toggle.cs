using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scrips allows player to toggle the active state of a game object, it is useful for things like light switch.
/// 
/// State: If you edit this value it will affect the inital state of the target object.
/// Target: The object that you want the player to toggle.
/// 
/// </summary>

public class Item_Toggle : Interactable
{
    public bool state;
    public GameObject target;

    private void Awake()
    {
        if (target)
            target.SetActive(state);
    }

    public override void Interact()
    {
        state = !state;
        if (target)
            target.SetActive(state);
    }
}
