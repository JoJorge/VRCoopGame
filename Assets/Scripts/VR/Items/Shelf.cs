using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : Item {

    public override void interact () {
        if (transform.childCount == 0) {
            Vector3 pos = transform.localPosition;
            pos.x = -0.01f;
            transform.localPosition = pos;
        }
    }
}
