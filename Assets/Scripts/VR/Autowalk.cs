﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
This script was developed by Jupp Otto. It's free to use and there are no restrictions in modification.
If there are any questions you can send me an Email: juppotto3@gmail.com
This script moves your player automatically in the direction he is looking at. You can 
activate the autowalk function by pull the cardboard trigger, by define a threshold angle 
or combine both by selecting both of these options.
The threshold is an value in degree between 0° and 90°. So for example the threshold is 
30°, the player will move when he is looking 31° down to the bottom and he will not move 
when the player is looking 29° down to the bottom. This script can easally be configured
in the Unity Inspector. 
How to get started with this script?: 
0. haven't the Google VR SDK yet, follow this guide https://developers.google.com/vr/unity/get-started
1. Import the exampple package downloaded in step 0 (GoogleVRForUnity.unitypackage).
2. Load the GVRDemo scene.
3. Attach a Rigidbody to the "Player" GameObject.
4. Freeze X, Y and Z Rotation of the Rgidbody in the inspector. 
5. Attach a Capsule Collider to the "Player" GameObject.
6. Attach the Autowalk script to the "Player" GameObject.
7. Configure the Script in the insector for example: 
      - walk when triggered   = true 
      - speed                 = 3  
8. Make sure your ground have a collider on it. (Otherwise you will fall through it)
9. Start the scene and have fun! 
*/


public class Autowalk : MonoBehaviour
{
    private const int RIGHT_ANGLE = 90;

    // This variable determinates if the player will move or not
    
    public bool isWalking = false;
    Transform mainCamera = null;

    //This is the variable for the player speed
    [Tooltip("With this speed the player will move.")]
    public float speed;

    [Tooltip("Activate this checkbox if the player shall move when the Cardboard trigger is pulled.")]
    public bool walkWhenTriggered;

    [Tooltip("Activate this checkbox if the player shall move when he looks below the threshold.")]
    public bool walkWhenLookDown;

    [Tooltip("This has to be an angle from 0° to 90°")]
    public double thresholdAngle;

    [Tooltip("Activate this Checkbox if you want to freeze the y-coordiante for the player. " +
             "For example in the case of you have no collider attached to your CardboardMain-GameObject" +
             "and you want to stay in a fixed level.")]
    public bool freezeYPosition;

    [Tooltip("This is the fixed y-coordinate.")]
    public float yOffset;

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        double x = mainCamera.transform.eulerAngles.x;
        // let the angle between -180 to 180
        if (x > 180) {  x = x - 360.0;  }

        
        // Walk when the Cardboard Trigger is used 
        if (walkWhenTriggered && !walkWhenLookDown && !isWalking && GvrViewer.Instance.Triggered)
        {
            isWalking = true;
        }
        else if (walkWhenTriggered && !walkWhenLookDown && isWalking && GvrViewer.Instance.Triggered)
        {
            isWalking = false;
        }

        // Walk when player looks below the threshold angle 
        if (walkWhenLookDown && !walkWhenTriggered && !isWalking &&
            x >= thresholdAngle &&
            x <= RIGHT_ANGLE)
        {
            isWalking = true;
        }
        else if (walkWhenLookDown && !walkWhenTriggered && isWalking &&
                 (x <= thresholdAngle ||
                 x >= RIGHT_ANGLE))
        {
            isWalking = false;
        }
        
        // Debug.Log(GvrViewer.Instance.HeadPose.Orientation);

        // Walk when the Cardboard trigger is used and the player looks down below the threshold angle
        if (walkWhenLookDown && walkWhenTriggered && !isWalking &&
            x >= thresholdAngle &&
            GvrViewer.Instance.Triggered &&
            x <= RIGHT_ANGLE)
        {
            isWalking = true;
            GetComponentInChildren<PhysicsRaycaster>().enabled = false;
        }
        else if (walkWhenLookDown && walkWhenTriggered && isWalking &&
                 (GvrViewer.Instance.Triggered ||
                 x >= RIGHT_ANGLE))
        {
            isWalking = false;
            GetComponentInChildren<PhysicsRaycaster>().enabled = true;
        }

        if (isWalking)
        {
            Vector3 direction = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z).normalized * speed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
            transform.Translate(rotation * direction);
        }

        if (freezeYPosition)
        {
            transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
        }
    }
}
