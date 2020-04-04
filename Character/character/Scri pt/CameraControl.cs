using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float mousesensitivity = 10f;
    public Transform target;
    public float distancefromtarget = 2f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float rotationSmoothTime = 0.12f;

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

   
    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mousesensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mousesensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distancefromtarget;
    }
}
