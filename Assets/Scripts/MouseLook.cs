using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    
    /**
     * This is applied twice to the player object.
     *
     * Only horizontal rotation is applied to the player object.
     * Only vertical rotation is applied to the camera object.
     *
     * MouseXAndY is for testing.
     *
     * Gravity is applied in FPSInput script. For gravity to work properly
     * along local coordinates, player must not tilt.
     * To make this possible, vertical rotation is disabled on the player.
     *
     * We do need vertical rotation so it is instead applied on the camera.
     * 
     */
    
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseX;
        
    // Start is called before the first frame update
    void Start()
    {
        /*
         * RigidBody is the component which the Physics engine interacts with.
         * We want to control the player solely by mouse and so we do not want
         * the Physics Engine to affect its rotation.
         */
        var body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }

    }

    public float horizontalSensitivity = 3.0f;
    public float verticalSensitivity = 3.0f;
    // Update is called once per frame

    public float verticalAngleMin = -45.0f;
    public float verticalAngleMax = 45.0f;
   
    private float pitch = 0;
    private float yaw = 0;
    private float roll = 0;
    
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            yaw = Input.GetAxis("Mouse X") * horizontalSensitivity;
            // Rotate function can only increment rotation angle. 
            // Default accumulation by adding angle
            transform.Rotate(0, yaw, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            // Angle is accumulated within [-45, 45] degrees.
            // Subtracting because by default angle comes inverse.
            // To understand, try executing with only the following line.
            // transform.Rotate(Input.GetAxis("Mouse X") * verticalSensitivity, 0, 0);
            
            pitch -= Input.GetAxis("Mouse Y") * verticalSensitivity;
            pitch = Mathf.Clamp(pitch, verticalAngleMin, verticalAngleMax);
                
            // We have to reassign angles instead of using transform.Rotate because,
            // Rotate will add this angle to the current orientation. And this will cross the limit we want to set.
            // Logic similar to Rotate, modified for our purpose is written above.
                
            yaw = transform.localEulerAngles.y;
            // localEulerAngles is a reference to a constant, so you must reassign.
            
            transform.localEulerAngles = new Vector3(pitch, yaw, 0);
        }
        else
        {
            
            /**
             * Approach is very similar.
             * We calculate pitch exactly similar to MouseY shown above
             * For yaw, we need to emulate functionality of transform.Rotate.
             * So we take the localEulerAngle and just add the current mouseX value.
             */
            
            var mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
            var mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;

            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, verticalAngleMin, verticalAngleMax);

           
            yaw = transform.localEulerAngles.y + mouseX;
            
            transform.localEulerAngles = new Vector3(pitch, yaw, 0);
        }
    }
}