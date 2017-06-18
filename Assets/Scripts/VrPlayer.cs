using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrPlayer : MonoBehaviour {

    private NetworkBridge bridge;
    private List<Item> itemList;
    private Item itemOnHand;
    private CameraSystem cameraSystem;
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
        itemList = new List<Item>();
        cameraSystem = GameObject.Find ("CameraSystem").GetComponent<CameraSystem>();
	}
	
	// Update is called once per frame
	void Update () {
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
    private void walk() {
    }
    private void stop() {
    }
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
    }
}
