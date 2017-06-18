using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingDevice : Item {

    public override void Start (){
        base.Start ();
        pickable = true;
        interactingItems.Add (GameObject.Find("ElevatorPanel"), new HackingStrategy());
        interactingItems.Add (GameObject.Find("PC"), new HackingStrategy());
        interactingItems.Add (GameObject.Find("CameraSystem"), new HackingStrategy());
    }
	
	
}
