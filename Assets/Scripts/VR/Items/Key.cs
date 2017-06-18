using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item {

    public override void Start () {
        base.Start ();
        pickable = true;
        interactingItems.Add (GameObject.Find("DataRoomDoor"), new OpenDoorStrategy());
    }
}
