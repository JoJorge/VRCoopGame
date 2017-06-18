using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : Item {

    public override void interact () {
        if (transform.childCount == 0) {
            // TODO 
            // push the shelf aside
        }
    }
}
