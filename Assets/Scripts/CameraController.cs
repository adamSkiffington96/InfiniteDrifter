using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public DriftCar car;

    public float velocityDistanceModifier = 1f;

    public float mouseCamRotationSpeed = 1f;

    public float autoCamRotationSpeed = 1f;

    public Transform cameraTarget;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, car.transform.position + (velocityDistanceModifier * car.nextVelocity), cameraSmoothing * Time.deltaTime);

        transform.position = car.transform.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(cameraTarget.forward, car.transform.up), autoCamRotationSpeed * Time.deltaTime);

        cameraTarget.Rotate(Vector3.up, Input.GetAxis("Mouse X") * mouseCamRotationSpeed * Time.deltaTime);

        //transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * mouseCamRotationSpeed * Time.smoothDeltaTime);

    }
}
