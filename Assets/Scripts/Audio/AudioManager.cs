using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioPlayerPrefab = null;

    /// <summary>
    /// Play audio from a source attached to the given transform, at the given volume (0-1).
    /// </summary>
    public static Action<Transform, AudioClip, float> playAudioAtTransform;

    /// <summary>
    /// Play audio from a source positioned at the given point, at the given volume (0-1).
    /// </summary>
    public static Action<Vector3, AudioClip, float> playAudioAtPoint;

    private void Start()
    {
        //Whenever any of the play audio actions are invoked, spawn an audio player there.
        playAudioAtTransform += OnPlayAudioAtTransform;
        playAudioAtPoint += OnPlayAudioAtPoint;
    }
    private void OnDestroy()
    {
        playAudioAtTransform -= OnPlayAudioAtTransform;
        playAudioAtPoint -= OnPlayAudioAtPoint;
    }

    private void OnPlayAudioAtTransform(Transform destinationTransform, AudioClip clipToPlay)
    {
        OnPlayAudioAtTransform(destinationTransform, clipToPlay, -1);
    }
    private void OnPlayAudioAtTransform(Transform destinationTransform, AudioClip clipToPlay, float volume = -1)
    {
        if (volume > 1)
        {
            volume = 1;
            Debug.LogWarning("PlayAudioAtTransform's volume parameter should be between 0 and 1. " +
                "Volume given: " + volume);
        }

        //Spawn an audio player attached to the desired transform and set its properties.
        //The player should play and destroy itself automatically.
        AudioSource spawnedAudioPlayer = Instantiate(audioPlayerPrefab, destinationTransform);
        spawnedAudioPlayer.clip = clipToPlay;
        //Only set volume if it's not the default value (-1).
        if (volume >= 0) { spawnedAudioPlayer.volume = volume; }
    }

    private void OnPlayAudioAtPoint(Vector3 destinationPoint, AudioClip clipToPlay)
    {
        OnPlayAudioAtPoint(destinationPoint, clipToPlay, -1);
    }
    private void OnPlayAudioAtPoint(Vector3 destinationPoint, AudioClip clipToPlay, float volume = -1)
    {
        if (volume > 1)
        {
            volume = 1;
            Debug.LogWarning("PlayAudioAtPoint's volume parameter should be between 0 and 1. " +
                "Volume given: " + volume);
        }

        //Spawn an audio player attached to the desired transform and set its properties.
        //The player should play and destroy itself automatically.
        AudioSource spawnedAudioPlayer = Instantiate(audioPlayerPrefab, destinationPoint, Quaternion.identity);
        spawnedAudioPlayer.clip = clipToPlay;
        //Only set volume if it's not the default value (-1).
        if (volume >= 0) { spawnedAudioPlayer.volume = volume; }
    }
}
