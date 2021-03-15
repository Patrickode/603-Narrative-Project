using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Hovering Outline Effect")]
    [Range(0f, 0.1f)] public float hoveringOutlineWidth = 0.015f;
    public Color hoveringOutlineColor;
    public List<Renderer> objectRenderers;

    protected List<Material> outlineMats;

    public abstract void Interact();

    [Header("Interactions")]
    public bool isInteracting;

    private void OnMouseOver()
    {
        if (outlineMats == null)
        {
            outlineMats = new List<Material>();

            // Get outline materials
            for (int i = 0; i < objectRenderers.Count; i++)
            {
                for (int j = 0; j < objectRenderers[i].materials.Length; j++)
                {
                    var currMat = objectRenderers[i].materials[j];
                    if (currMat.shader.name == "Custom/ObjectOutline")
                        outlineMats.Add(currMat);
                }
            }
        }
        else
        {
            for (int i = 0; i < outlineMats.Count; i++)
            {
                outlineMats[i].SetColor("_SecondOutlineColor", hoveringOutlineColor);
                outlineMats[i].SetFloat("_SecondOutlineWidth", hoveringOutlineWidth);
            }
        }
    }

    private void OnMouseExit()
    {
        if (outlineMats != null && outlineMats.Count > 0)
        {
            for (int i = 0; i < outlineMats.Count; i++)
            {
                outlineMats[i].SetFloat("_SecondOutlineWidth", 0);
            }
        }
    }
}
