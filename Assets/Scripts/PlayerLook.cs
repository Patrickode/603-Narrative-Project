using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public static PlayerLook instance;

    //View Sensitivity
    public float mouseXSensitivity = 100f;
    public float mouseYSensitivity = 100f;

    //Player transform
    public Transform player;

    //Rotation of x axis
    public float xRotation = 0f;

    public bool isDisabled = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //Disable cursor
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = 0f;
        float mouseY = 0f;

        if (!isDisabled)
        {
            //Get player inputs
            mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;


            //Calculate y-axis input, clamp if needed
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //Apply y-axis rotation
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            //Apply x-axis input
            player.Rotate(Vector3.up * mouseX);
        }
    }
}
