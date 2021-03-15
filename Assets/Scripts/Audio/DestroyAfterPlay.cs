using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour
{
    [SerializeField] private AudioSource thisASource = null;

    private void Start()
    {
        StartCoroutine(PlayThenDestroy());
    }

    private IEnumerator PlayThenDestroy()
    {
        thisASource.Play();
        //Wait until the audio source is playing.
        while (!thisASource.isPlaying) { yield return null; }
        //Then wait until it's done playing,
        while (thisASource.isPlaying) { yield return null; }
        //And once it is, destroy this game object.
        Destroy(gameObject);
    }
}
