using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotate : MonoBehaviour
{
    [SerializeField] private Transform rotatingThing;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        rotatingThing.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.back);
    }

    public void Invert()
    {
        rotationSpeed *= -1;
        rotatingThing.rotation = Quaternion.identity;
    }
}
