using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingStrategy : UseStrategy {

    public void use(GameObject obj) {
        VrPlayer.getInstance ().send ("hack", obj.name);
    }
}
