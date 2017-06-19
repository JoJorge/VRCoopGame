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
        pick(GameObject.Find("Toolbox").GetComponent<Item>());
        pick(GameObject.Find("HackingDevice").GetComponent<Item>());
        // cameraSystem = GameObject.Find ("CameraSystem").GetComponent<CameraSystem>();

        
    }
	
	// Update is called once per frame
	void Update () {
        double x = Camera.main.transform.eulerAngles.x;
        // let the angle between -180 to 180
        if (x > 180) { x = x - 360.0; }
        if (touchUI.getIsDisplayed() == false && x <= thresholdAngle && GvrViewer.Instance.Triggered) {
            touchUI.DisplayUI(x, itemList); // the number 3 is for temp test, need to be fix
        }
        /*
        // get camera views
        if (cameraSystem.isTurnedOn ()) {
            if (Time.time - prvTime >= refreshTime) {
                Texture2D[] images = cameraSystem.getImage ();
                for(int i = 0; i < images.Length; i++) {
                    send ("camera " + i, images[i]);
                }
                prvTime = Time.time;
            }
        }
        */
    }

    public void setBridge(NetworkBridge brdg) {
        bridge = brdg;
    }
    public void send(string type, string content) {
        bridge.sendToStr(type, content);
    }
    public void send(string type, Texture content) {
        //bridge.CmdSendToPcImg(type, content);
    }

    public void receive(string type, string content) {

        switch (type) {
        case "camera":
            if (content == "on") {
                cameraSystem.turnOn ();
                prvTime = Time.time;
            }
            break;
        case "elevator":
            string[] ops = content.Split (' ');
            int floorNum;
            if (ops.Length == 2 && ops [0] == "floor" && int.TryParse(ops[1], out floorNum)) {
                if (floorNum == 4) {
                    StartCoroutine(GameObject.Find ("ElevatorDoor").GetComponent<ElevatorDoor>().openDoor());
                }
            } 
            else {
                Debug.Log ("wrong msg! - " + type + "/" + content);
            }
            break;
        }
    }
    public void pick(Item item) {
        itemList.Add (item);
        item.gameObject.transform.SetParent(Camera.main.transform);
        item.gameObject.SetActive (false);
        item.transform.localPosition = Vector3.zero;
    }
    public void drop(Item item) {
        if (itemOnHand == item) {
            unhold ();
        }
        itemList.Remove (item);
        item.gameObject.SetActive (true);
        item.transform.SetParent (null);
    }
    public void hold(Item item) {
        unhold ();
        itemOnHand = item;
        item.gameObject.SetActive (true);
        item.transform.localPosition = new Vector3(0.3f, -0.3f, 0.5f);
    }
    public void unhold() {
        if (itemOnHand) {
            itemOnHand.gameObject.SetActive (false);
            itemOnHand = null;
        }
    }
    public void interact(GameObject obj) {
        Debug.Log(obj.name);
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
