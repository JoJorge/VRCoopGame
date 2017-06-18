using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    public Sprite icon;
    protected bool pickable;
    protected Dictionary<GameObject, UseStrategy> interactingItems;
    protected VrPlayer vrPlayer;

	// Use this for initialization
	public virtual void Start () {
        vrPlayer = VrPlayer.getInstance ();
        interactingItems = new Dictionary<GameObject, UseStrategy> ();
        pickable = false;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
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
    public virtual void trigger() {
        vrPlayer.interact (gameObject);
    }
    public virtual void interact() {
    }
    protected void send(string type, string content) { 
        vrPlayer.send (type, content);
    }
}
