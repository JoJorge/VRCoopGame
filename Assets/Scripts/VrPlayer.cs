using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrPlayer : MonoBehaviour {

    private NetworkBridge bridge;
    private TouchUI touchUI; 
    private List<Item> itemList;
    private Item itemOnHand;
    private CameraSystem cameraSystem;
    private double thresholdAngle = -25;
    private float prvTime;
    private const float refreshTime = 5.0f;
    private static VrPlayer instance;

    public static VrPlayer getInstance() {
        if (instance == null)
            instance = FindObjectOfType (typeof(VrPlayer)) as VrPlayer;
        return instance;
    }

	// Use this for initialization
	void Start () {
        if (getInstance () != this) {
            Destroy (this);
        }
        bridge = FindObjectOfType (typeof(NetworkBridge)) as NetworkBridge;
        touchUI = GameObject.Find("Canvas").GetComponent<TouchUI>();
        itemList = new List<Item>();
        cameraSystem = GameObject.Find ("CameraSystem").GetComponent<CameraSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        double x = Camera.main.transform.eulerAngles.x;
        // let the angle between -180 to 180
        if (x > 180) { x = x - 360.0; }
        if (touchUI.getIsDisplayed() == false && x <= thresholdAngle && GvrViewer.Instance.Triggered) {
            touchUI.DisplayUI(x, 3); // the number 3 is for temp test, need to be fix
        }

        // get camera views
        if (cameraSystem.isTurnedOn ()) {
            if (Time.time - prvTime >= refreshTime) {
                Sprite[] images = cameraSystem.getImage ();
                for(int i = 0; i < images.Length; i++) {
                    send ("camera " + i, images[i]);
                }
                prvTime = Time.time;
            }
        }
	}

    public void send(string type, string content) {
        bridge.CmdSendToPcStr(type, content);
    }
    public void send(string type, Sprite content) {
        //bridge.CmdSendToPcImg(type, content);
    }
    public void receive(string type, string content) {
        Debug.Log ("VR received");

        switch (type) {
        case "camera":
            if (content == "on") {
                cameraSystem.turnOn ();
                prvTime = Time.time;
            }
            break;
        }
    }
    // Autowalk.cs provides the behavior 
    /*
    private void walk() {

    }
    
    private void stop() {
    }
    */
    public void pick(Item item) {
        itemList.Add (item);
        item.gameObject.transform.SetParent(transform);
        item.gameObject.SetActive (false);
        item.transform.localPosition = Vector3.zero;
    }
    private void hold(Item item) {
        itemOnHand = item;
    }
    public void interact(GameObject obj) {
        if (itemOnHand != null) {
            itemOnHand.use (obj);
        }
        else if (obj.GetComponent<Item> () != null) {
            Item item = obj.GetComponent<Item> ();
            if (item.isPickable ()) {
                pick (item);
            }
            else {
                item.interact ();
            }
        }
    }
    private void showItemList() {
    }
    public void hideItemList() {
        touchUI.disableUI();
    }
}
