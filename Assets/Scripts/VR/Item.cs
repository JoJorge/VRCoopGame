using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    private bool pickable;
    private Dictionary<GameObject, UseStrategy> interactingItems;
    private VrPlayer vrPlayer;

	// Use this for initialization
	void Start () {
        vrPlayer = VrPlayer.getInstance ();
        interactingItems = new Dictionary<GameObject, UseStrategy> ();
        pickable = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool isPickable() {
        return pickable;
    }
    public void use(GameObject obj) {
        if (interactingItems.ContainsKey (obj)) {
            UseStrategy st = interactingItems [obj];
            st.use (obj);
        }
    }
    public virtual void use() {
    }
    public void trigger() {
        vrPlayer.interact (gameObject);
    }
    public virtual void interact() {
    }
    protected void send(string type, string content) { 
        vrPlayer.send (type, content);
    }
}
