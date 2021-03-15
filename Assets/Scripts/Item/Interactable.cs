using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] [Range(0f, 0.05f)] public float hoveringOutlineWidth = 0;

    public abstract void Interact();
    public bool isInteracting;

    private void OnMouseEnter()
    {
        if (gameObject.GetComponent<Renderer>())
        {
            gameObject.GetComponent<Renderer>().material.SetFloat("_SecondOutlineWidth", hoveringOutlineWidth);
        }
    }

    private void OnMouseExit()
    {
        if (gameObject.GetComponent<Renderer>())
        {
            gameObject.GetComponent<Renderer>().material.SetFloat("_SecondOutlineWidth", 0);
        }
    }
}
