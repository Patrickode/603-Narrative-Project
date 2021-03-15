using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transitions : MonoBehaviour
{
    [SerializeField] private CanvasGroup fader;
    [SerializeField] private AudioClip introClip;

    public static Action<bool> Fading;

    private void Start()
    {
        StartCoroutine(FadeIn(introClip.length * 0.3f, introClip.length * 0.7f));
    }

    private IEnumerator FadeIn(float waitTime, float fadeTime)
    {
        //Wait a frame so the Fading action will invoke properly
        yield return null;

        //Disable movement and the crosshair, then play the intro audio.
        Fading?.Invoke(true);
        PlayerMovement.instance.isDisabled = true;
        AudioSource.PlayClipAtPoint(introClip, PlayerCam.instance.cam.transform.position, 1);

        //Start a fade that'll last until the audio clip is done. Once it's finished, enable the player's movement and crosshair.
        yield return StartCoroutine(FadeAfter(waitTime, true, fadeTime));
        PlayerMovement.instance.isDisabled = false;
        Fading?.Invoke(false);
    }

    private IEnumerator FadeAfter(float waitTime, bool fadingIn, float fadeTime)
    {
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(Fade(fadingIn, fadeTime));
    }

    private IEnumerator Fade(bool fadingIn, float fadeTime)
    {
        float fadeProgress = 0;
        while (fadeProgress < 1)
        {
            fadeProgress += Time.deltaTime / fadeTime;

            float newAlpha;
            if (fadingIn) { newAlpha = Mathf.Lerp(1, 0, fadeProgress); }
            else { newAlpha = Mathf.Lerp(0, 1, fadeProgress); }

            fader.alpha = newAlpha;

            yield return null;
        }
    }
}
