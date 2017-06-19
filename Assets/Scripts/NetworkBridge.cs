using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkBridge : NetworkBehaviour {

    private PcPlayer pcPlayer;
    private VrPlayer vrPlayer;

	// Use this for initialization
	void Start () {
        pcPlayer = PcPlayer.getInstance ();
        vrPlayer = VrPlayer.getInstance ();
        if (pcPlayer) {
            pcPlayer.setBridge (this);
        }
        if (vrPlayer) {
            vrPlayer.setBridge (this);
        }
    }
    public void sendToStr(string type, string content) {
        if (isLocalPlayer) {
            CmdSendToPcStr(type, content);
        }
        else {
            Debug.Log("not a local player !");
        }
    }
    [Command]
    public void CmdSendToPcStr(string type, string content) {
        pcPlayer.receive (type, content);
    }
    /*
    [Command]
    public void CmdSendToPcImg(string type, Texture content) {
        pcPlayer.receive (type, content);
    }
    */
    [ClientRpc]
    public void RpcSendToVr(string type, string content) {
        vrPlayer.receive (type, content);
    }
}
