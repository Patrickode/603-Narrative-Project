using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    public static PlayerCam instance;

    public Camera cam;
    private void Awake()
    {
        instance = this;
    }
}
