using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingDevice : Item {

    public override void Start (){
        base.Start ();
        pickable = true;
        interactingItems.Add (GameObject.Find("ElevatorPanel"), new HackingStrategy(this, new Vector3(0, 0, 0), new Vector3(0, 0, 0)));
        interactingItems.Add (GameObject.Find("PC"), new HackingStrategy(this, new Vector3(0, 0, 0), new Vector3(0, 0, 0)));
        interactingItems.Add (GameObject.Find("CameraSystem"), new HackingStrategy(this, new Vector3(0, 0, 0), new Vector3(0, 0, 0)));
    }
	
}
