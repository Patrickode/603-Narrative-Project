using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ImageRendererdEffect : MonoBehaviour
{
    public Material processingImageMaterial;
    public bool triggerRandomness = true;

    // Update is called once per frame
    void Update()
    {
        if (triggerRandomness)
        {
            processingImageMaterial.SetFloat("_RandomNumber", Random.Range(0.0f, 1.0f));
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (processingImageMaterial != null)
        {
            Graphics.Blit(source, destination, processingImageMaterial);
        }
    }
}
