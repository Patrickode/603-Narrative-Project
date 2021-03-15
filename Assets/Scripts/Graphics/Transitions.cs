using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transitions : MonoBehaviour
{
    [SerializeField] private CanvasGroup fader;
    [SerializeField] private CanvasGroup outerFader;
    [SerializeField] private AudioClip introClip;
    [SerializeField] private AudioClip outroClip;
    [SerializeField] private ConstantRotate rotatingCassette;

    private HashSet<string> interactedObjects;

    public static Action<bool> Fading;

    private void Start()
    {
        interactedObjects = new HashSet<string>();
        StartCoroutine(FadeIn(introClip.length * 0.3f, introClip.length * 0.7f));
        Item_ZoomIn.FinishedInteracting += CheckInteractedObjects;
    }
    private void OnDestroy() { Item_ZoomIn.FinishedInteracting -= CheckInteractedObjects; }

    private void CheckInteractedObjects(string interactedObject)
    {
        interactedObjects.Add(interactedObject);
        Debug.Log(interactedObject);
        if (interactedObjects.Count >= 6)
        {
            StartCoroutine(FadeOut(0, outroClip.length));
        }
    }

    private IEnumerator FadeIn(float waitTime, float fadeTime)
    {
        //Wait a frame so the Fading action will invoke properly
        yield return null;

        //Disable movement and the crosshair, then play the intro audio.
        Fading?.Invoke(true);
        PlayerMovement.instance.isDisabled = true;
        AudioSource.PlayClipAtPoint(introClip, PlayerCam.instance.cam.transform.position, 0.8f);

        //Start a fade that'll last until the audio clip is done. Once it's finished, enable the player's movement and crosshair.
        StartCoroutine(FadeAfter(outerFader, 2, true, 5));
        yield return StartCoroutine(FadeAfter(fader, waitTime, true, fadeTime));
        PlayerMovement.instance.isDisabled = false;
        Fading?.Invoke(false);
    }

    private IEnumerator FadeOut(float waitTime, float fadeTime)
    {
        rotatingCassette.Invert();
        yield return null;

        Fading?.Invoke(true);
        PlayerMovement.instance.isDisabled = true;
        AudioSource.PlayClipAtPoint(outroClip, PlayerCam.instance.cam.transform.position, 0.8f);

        //Start a fade that'll last until the audio clip is done. Once it's finished, wait a little, then quit the game.
        yield return StartCoroutine(FadeAfter(fader, waitTime, false, fadeTime));
        yield return StartCoroutine(Fade(outerFader, false, 1));
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    private IEnumerator FadeAfter(CanvasGroup thingToFade, float waitTime, bool fadingIn, float fadeTime)
    {
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(Fade(thingToFade, fadingIn, fadeTime));
    }

    private IEnumerator Fade(CanvasGroup thingToFade, bool fadingIn, float fadeTime)
    {
        float fadeProgress = 0;
        while (fadeProgress < 1)
        {
            fadeProgress += Time.deltaTime / fadeTime;

            float newAlpha;
            if (fadingIn) { newAlpha = Mathf.Lerp(1, 0, fadeProgress); }
            else { newAlpha = Mathf.Lerp(0, 1, fadeProgress); }

            thingToFade.alpha = newAlpha;

            yield return null;
        }
    }
}
