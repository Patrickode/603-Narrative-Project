using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] [Range(0f, 0.05f)] private float hoveringOutlineWidth = 0.0015f;

    public abstract void Interact();
    public bool isInteracting;
}
