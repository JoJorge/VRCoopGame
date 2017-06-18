using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

    private bool isOn;

	// Use this for initialization
	void Start () {
        isOn = false;
	}
	
    public bool isTurnedOn() {
        return isOn;
    }
    public void turnOn() {
        isOn = true;
    }
    public Sprite[] getImage() {
        // TODO
        // get images in all the cameras
        return new Sprite[6];
    }
}
