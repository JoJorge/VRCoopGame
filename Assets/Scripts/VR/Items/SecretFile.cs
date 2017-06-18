using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretFile : Item {

    public override void Start ()
    {
        base.Start ();
        pickable = true;
    }
    public override void trigger ()
    {
        vrPlayer.pick (this);
        // TODO
        // trigger the alarm
    } 
}
