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
    public Texture2D[] getImage() {
        // TODO
        // get images in all the cameras
        Texture2D[] txs = new Texture2D[1];
        RenderTexture rtx = GetComponentInChildren<MeshRenderer>().material.mainTexture as RenderTexture;
        RenderTexture.active = rtx;
        txs [0] = new Texture2D (rtx.width, rtx.height);
        txs [0].ReadPixels (new Rect(0, 0, rtx.width, rtx.height), 0, 0);
        txs [0].Apply ();
        RenderTexture.active = null;
        if (txs [0] == null)
            Debug.Log ("??");
        return txs;
    }
}
