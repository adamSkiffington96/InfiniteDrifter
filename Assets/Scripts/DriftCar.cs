using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriftCar : MonoBehaviour
{
    public float driveSpeed = 1f;

    public float acceleration = 1f;
    public float turnAcceleration = 1f;

    public Vector3 nextVelocity = Vector3.zero;

    public float turnSpeed = 1f;

    private float nextTurnAmount = 0f;

    public Transform cameraTransform;

    public Transform[] FrontWheels;

    public Transform[] AllWheels;

    private float lastFrontWheelRotation = 0f;

    public float frontWheelSteerModifier = 2f;

    public float wheelsSpinningSpeed = 1f;

    private float bodyTilt = 0f;
    public float bodyTiltSpeed = 1f;

    public float bodyTiltAmount = 2f;

    public Transform body;

    private float differenceFriction = 0f;

    public Text debugText;

    public LineRenderer debugForwardLine;
    public LineRenderer debugVelocityLine;

    public bool showDebugLines = false;

    public float minimumFrictionAmount = 0.75f;


    private void Start()
    {
        
    }

    private void Update()
    {
        ControlMovement();

        ControlWheels();

        TiltingBehavior();
    }

    private void ControlMovement()
    {
        Vector3 inputVect = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        differenceFriction = Mathf.Clamp((nextVelocity.normalized - transform.forward.normalized).magnitude, 0, 2f);

        float friction = 2f - differenceFriction;

        debugText.text = friction.ToString();

        nextVelocity = Vector3.Lerp(nextVelocity, Mathf.Clamp((friction / 2f), 0.87f, 1f) * driveSpeed * transform.forward.normalized, Mathf.Clamp((friction / 2f), 0.2f, 1f) * acceleration * Time.deltaTime);

        if(showDebugLines)
            DebugBehavior();

        transform.position = transform.position + (nextVelocity * Time.deltaTime);
        

        nextTurnAmount = Mathf.Lerp(nextTurnAmount, Mathf.Clamp((2f - friction) / 1.7f, minimumFrictionAmount, 2f) * inputVect.x * turnSpeed, turnAcceleration * Time.deltaTime);

        transform.Rotate(transform.up, nextTurnAmount * Time.deltaTime);
    }

    private void DebugBehavior()
    {
        //debugText.text = differenceFriction.ToString();

        Vector3 offset = new Vector3(0f, 1.4f, 0f);

        Vector3 originPos = transform.position + offset;
        Vector3 forwardPos = originPos + transform.forward.normalized;
        Vector3 velocityPos = originPos + nextVelocity.normalized;

        Debug.DrawLine(originPos, forwardPos, Color.green);
        Debug.DrawLine(originPos, velocityPos, Color.red);

        debugForwardLine.SetPosition(0, originPos + (transform.forward.normalized * 0.05f));
        debugForwardLine.SetPosition(1, forwardPos);

        debugVelocityLine.SetPosition(0, originPos + (nextVelocity.normalized * 0.05f));
        debugVelocityLine.SetPosition(1, velocityPos);
    }

    private void TiltingBehavior()
    {
        bodyTilt = Mathf.Lerp(bodyTilt, Input.GetAxis("Horizontal") * bodyTiltAmount, bodyTiltSpeed * Time.deltaTime);

        body.localEulerAngles = new Vector3(0, 0, bodyTilt);
    }

    private void ControlWheels()
    {
        for (int i = 0; i < 4; i++) {

            float speed = wheelsSpinningSpeed;
            if (i < 2)
                speed /= 2;

            AllWheels[i].Rotate(new Vector3(speed * Time.deltaTime * 6f, 0f, 0f));
            //AllWheels[i].Rotate(new Vector3((60f / wheelsSpinningSpeed) * Time.deltaTime, 0f, 0f));
        }

        lastFrontWheelRotation = Mathf.Lerp(lastFrontWheelRotation, 45 * Input.GetAxis("Horizontal"), frontWheelSteerModifier * turnAcceleration * Time.deltaTime);

        for (int i = 0; i < 2; i++) {
            Vector3 yRot = FrontWheels[i].localEulerAngles;
            yRot.y = lastFrontWheelRotation;
            FrontWheels[i].localEulerAngles = yRot;
        }
    }
}
