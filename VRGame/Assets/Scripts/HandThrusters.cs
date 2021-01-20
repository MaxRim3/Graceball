using System;
using UnityEngine;

public class HandThrusters : MonoBehaviour
{
    public Transform primaryCamera;
    public Rigidbody body;
    public Rigidbody leftHand;
    public Rigidbody rightHand;
    public OVRInput.Controller leftController;
    public OVRInput.Controller rightController;
    private readonly OVRInput.Axis1D thruster = OVRInput.Axis1D.PrimaryIndexTrigger;
    private readonly OVRInput.Axis1D reverseThruster = OVRInput.Axis1D.PrimaryHandTrigger;
    private readonly OVRInput.Button thumb = OVRInput.Button.One;
    private readonly OVRInput.RawAxis2D thumbstickL = OVRInput.RawAxis2D.LThumbstick;
    private readonly OVRInput.RawAxis2D thumbstickR = OVRInput.RawAxis2D.RThumbstick;
    private bool rocketLeftEnabled;
    public AudioSource rocketSoundLeft;
    public ParticleSystem rocketEffectLeft;
    private bool rocketRightEnabled;
    public AudioSource rocketSoundRight;
    public ParticleSystem rocketEffectRight;

    private readonly float thrustForce = 10f;
    private readonly float maxVelocity = 20f;
    private readonly float maxUpwardsVelocity = 4f;

    private readonly float torque = 1f;

    public void Start()
    {
        //leftHand = GameObject.FindWithTag("LeftCustomHand").GetComponent<Rigidbody>();
        //rightHand = GameObject.FindWithTag("RightCustomHand").GetComponent<Rigidbody>();
        leftHand = GameObject.Find("CustomHandLeft").GetComponent<Rigidbody>();
        rightHand = GameObject.Find("CustomHandRight").GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, body.gameObject.transform.eulerAngles.y, 0);
        // Add force based on where the camera is relative to controllers
        Vector3 cameraPosition = primaryCamera.position;

         if(OVRInput.Get(thumb, leftController))
        {
            body.velocity = body.velocity * 0.98f;
            body.angularVelocity = body.angularVelocity * 0.98f;
            print("got press left");
        }

        if (OVRInput.Get(thumb, rightController))
        {
            body.velocity = body.velocity * 0.98f;
            body.angularVelocity = body.angularVelocity * 0.98f;
            print("got press right");
        }

        Vector2 leftThumbstickSwivel = OVRInput.Get(thumbstickL, leftController);
        Vector2 rightThumbstickSwivel = OVRInput.Get(thumbstickR, rightController);
        float turn = Input.GetAxis("Horizontal");
        
        body.AddTorque(transform.up * torque * leftThumbstickSwivel);
        body.AddTorque(-transform.up * torque * rightThumbstickSwivel);


        // Sqrt to convert to linear thrust since exponential is more difficult to control
        float leftPressure = (float)Math.Sqrt(OVRInput.Get(thruster, leftController));
        if (leftPressure > 0)
        {
            if (!rocketLeftEnabled)
            {
                rocketLeftEnabled = true;
                rocketSoundLeft.Play(0);
                rocketEffectLeft.Play(true);
            }
            OVRInput.SetControllerVibration(0, leftPressure / 6, leftController);
            rocketSoundLeft.volume = leftPressure / 2;
            //float leftThrustValue = thrustForce * leftPressure;
            //float forceX = leftThrustValue * (cameraPosition.x - leftHand.position.x);
            //float forceY = leftThrustValue * (cameraPosition.y - leftHand.position.y);
            //float forceZ = leftThrustValue * (cameraPosition.z - leftHand.position.z);
            //body.AddForce(forceX, forceY, forceZ);
            body.AddForce(leftHand.transform.forward * thrustForce);
        }
        else if (rocketLeftEnabled)
        {
            rocketLeftEnabled = false;
            rocketSoundLeft.Pause();
            rocketEffectLeft.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            OVRInput.SetControllerVibration(0, 0, leftController);
        }

		// Sqrt to convert to linear thrust since exponential is more difficult to control
		float rightPressure = (float)Math.Sqrt(OVRInput.Get(thruster, rightController));
        if (rightPressure > 0)
        {
            if (!rocketRightEnabled)
            {
                rocketRightEnabled = true;
                rocketSoundRight.Play(0);
                rocketEffectRight.Play(true);
            }
            OVRInput.SetControllerVibration(1, rightPressure / 6, rightController);
            rocketSoundRight.volume = rightPressure / 2;
            //float rightThrustValue = thrustForce * rightPressure;
            //float forceX = rightThrustValue * (cameraPosition.x - rightHand.position.x);
            //float forceY = rightThrustValue * (cameraPosition.y - rightHand.position.y);
            //float forceZ = rightThrustValue * (cameraPosition.z - rightHand.position.z);
            //body.AddForce(forceX, forceY, forceZ);
            body.AddForce(rightHand.transform.forward * thrustForce);
        }
        else if (rocketRightEnabled)
        {
            rocketRightEnabled = false;
            rocketSoundRight.Pause();
            rocketEffectRight.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            OVRInput.SetControllerVibration(0, 0, rightController);
        }




        float leftPressureR = (float)Math.Sqrt(OVRInput.Get(reverseThruster, leftController));
        if (leftPressureR > 0)
        {
            if (!rocketLeftEnabled)
            {
                rocketLeftEnabled = true;
                rocketSoundLeft.Play(0);
                rocketEffectLeft.Play(true);
            }
            OVRInput.SetControllerVibration(0, leftPressure / 6, leftController);
            rocketSoundLeft.volume = leftPressureR / 2;
            //float leftThrustValue = thrustForce * leftPressure;
            //float forceX = leftThrustValue * (cameraPosition.x - leftHand.position.x);
            //float forceY = leftThrustValue * (cameraPosition.y - leftHand.position.y);
            //float forceZ = leftThrustValue * (cameraPosition.z - leftHand.position.z);
            //body.AddForce(forceX, forceY, forceZ);
            body.AddForce(-leftHand.transform.forward * thrustForce);
        }
        else if (rocketLeftEnabled)
        {
            rocketLeftEnabled = false;
            rocketSoundLeft.Pause();
            rocketEffectLeft.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            OVRInput.SetControllerVibration(0, 0, leftController);
        }

        // Sqrt to convert to linear thrust since exponential is more difficult to control
        float rightPressureR = (float)Math.Sqrt(OVRInput.Get(reverseThruster, rightController));
        if (rightPressureR > 0)
        {
            if (!rocketRightEnabled)
            {
                rocketRightEnabled = true;
                rocketSoundRight.Play(0);
                rocketEffectRight.Play(true);
            }
            OVRInput.SetControllerVibration(1, rightPressureR / 6, rightController);
            rocketSoundRight.volume = rightPressure / 2;
            //float rightThrustValue = thrustForce * rightPressure;
            //float forceX = rightThrustValue * (cameraPosition.x - rightHand.position.x);
            //float forceY = rightThrustValue * (cameraPosition.y - rightHand.position.y);
            //float forceZ = rightThrustValue * (cameraPosition.z - rightHand.position.z);
            //body.AddForce(forceX, forceY, forceZ);
            body.AddForce(-rightHand.transform.forward * thrustForce);
        }
        else if (rocketRightEnabled)
        {
            rocketRightEnabled = false;
            rocketSoundRight.Pause();
            rocketEffectRight.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            OVRInput.SetControllerVibration(0, 0, rightController);
        }




        // Set max velocity
        float velocityX = body.velocity.x;
        if (Math.Abs(velocityX) > maxVelocity)
        {
            float brakeSpeed = (Math.Abs(velocityX) - maxVelocity) * (velocityX > 0 ? -1 : 1);
            body.AddForce(brakeSpeed, 0, 0);
        }

        // Only want to limit upwards velocity, not downwards. Allows gravity to
        // properly accelerate
        float velocityY = body.velocity.y;
        if (velocityY > maxUpwardsVelocity)
        {
            float brakeSpeed = velocityY - maxUpwardsVelocity;
            body.AddForce(0, -brakeSpeed, 0);
        }

        float velocityZ = body.velocity.z;
        if (Math.Abs(velocityZ) > maxVelocity)
        {
            float brakeSpeed = (Math.Abs(velocityZ) - maxVelocity) * (velocityZ > 0 ? -1 : 1);
            body.AddForce(0, 0, brakeSpeed);
        }

        // Set controller velocity so it doesn't studder
        leftHand.velocity = body.velocity;
        rightHand.velocity = body.velocity;
    }
}
