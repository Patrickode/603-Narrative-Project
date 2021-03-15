using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorByState : MonoBehaviour
{
    [SerializeField] private Image thisImage = null;
    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;

    private void Start()
    {
        Item_ZoomIn.DelayStartedOrEnded += OnDelayStartOrEnd;
        Transitions.Fading += OnDelayStartOrEnd;
    }
    private void OnDestroy()
    {
        Item_ZoomIn.DelayStartedOrEnded -= OnDelayStartOrEnd;
        Transitions.Fading -= OnDelayStartOrEnd;
    }

    private void OnDelayStartOrEnd(bool started)
    {
        //If delay has started, make the cursor color the disabled color. Otherwise, use the enabled color.
        thisImage.color = started ? disabledColor : enabledColor;
    }
}
