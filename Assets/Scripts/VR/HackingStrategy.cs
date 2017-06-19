using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingStrategy : UseStrategy {

    private HackingDevice device;
    private Vector3 placePos;
    private Vector3 placeRot;

    public HackingStrategy(HackingDevice device, Vector3 pos, Vector3 rot) {
        this.device = device;
        this.placePos = pos;
        this.placeRot = rot;
    }

    public void use(GameObject obj) {
        VrPlayer.getInstance ().drop (device);
        device.transform.position = placePos;
        Quaternion rot = device.transform.localRotation;
        rot.eulerAngles = placeRot;
        device.transform.rotation = rot;
        VrPlayer.getInstance ().send ("hack", obj.name);
        if (obj.name == "CameraSystem") {
            device.setPickable (false);
        }
    }
}
