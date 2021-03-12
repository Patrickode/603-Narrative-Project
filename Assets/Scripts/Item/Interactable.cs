using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();
    public bool isInteracting;
}
