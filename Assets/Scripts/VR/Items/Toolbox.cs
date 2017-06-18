using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : Item {

    public override void Start () {
        base.Start ();
        pickable = true;
        interactingItems.Add (GameObject.Find("PanelCover"), new OpenPanelStrategy());
    }
}
