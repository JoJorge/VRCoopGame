using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTest : MonoBehaviour {

    public CameraSystem cameraSystem;
    private float refreshTime = 1.0f;
    private float prvTime;
	// Use this for initialization
	void Start () {
        prvTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - prvTime >= refreshTime) {
            Texture2D tx = cameraSystem.getImage () [0];
            Image img = GetComponent<Image> ();
            if (img == null)
                Debug.Log ("img null");
            if (tx == null)
                Debug.Log ("tx null");
            img.sprite = Sprite.Create (tx, new Rect (0, 0, tx.width, tx.height), new Vector2 (0.5f, 0.5f));
            prvTime = Time.time;
        }
	}
}
