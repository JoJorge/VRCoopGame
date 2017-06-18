using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrPlayer : MonoBehaviour {

    private List<Item> itemList;
    private Item itemOnHand;
    private CameraSystem cameraSystem;
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
        itemList = new List<Item>();
        cameraSystem = GameObject.Find ("CameraSystem").GetComponent<CameraSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		// TODO
	}

    public void send(string type, string content) {
    }
    public void send(string type, Sprite content) {
    }
    public void receive(string type, string content) {
    }
    private void walk() {
    }
    private void stop() {
    }
    private void pick(Item item) {
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
