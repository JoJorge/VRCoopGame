using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcPlayer : MonoBehaviour {

    private NetworkBridge bridge;
    private GameObject slideBar;
    private GameObject mainScreen;
	[SerializeField]
    private App currentApp;
    [SerializeField]
    private List<App> apps;
    private static PcPlayer instance;

    public static PcPlayer getInstance() {
        if (instance == null)
            instance = FindObjectOfType (typeof(PcPlayer)) as PcPlayer;
        return instance;
    }

	// Use this for initialization
	void Start () {
        if (getInstance () != this) {
            Destroy (this);
        }
        // bridge = FindObjectOfType (typeof(NetworkBridge)) as NetworkBridge;
        apps = new List<App>();
        // get all apps
        GetComponentsInChildren<App>(true, apps);
        // activate misson file
	}
	
	// Update is called once per frame
	void Update () {
		
	}
        
    private App findAppByName(string appName) {
        foreach(App app in apps) {
            if (app.name == appName) {
                return app;
            }
        }
        return null;
    }
    public void setBridge(NetworkBridge brdg) {
        bridge = brdg;
    }
    public bool isCurrentApp(App app) {
        return app.Equals (currentApp);
    }

    public void send(string type, string content) {
        bridge.RpcSendToVr(type, content);
    }
    public void receive<T>(string type, T content) {
        Debug.Log(type + "/" + content);

        // receive message from VrPlayer
        string typeName;
        string header;
        if (type.Contains(" ")) {
            int idx = type.IndexOf(" ");
            typeName = type.Substring(0, idx);
            header = type.Substring(idx + 1);
        }
        else {
            typeName = type;
            header = "";
        }
        if (typeName == "hack") {
            App trml = findAppByName ("Terminal");
            if (trml.isActivated () == false) {
                trml.activate ();
            }
            trml.input ("", "hack " + content);
        }
        else if (typeName == "camera") {
            App mntr = findAppByName ("Monitor");
            mntr.input (header, content);
        }
    }
    private void showInSideBar(App app) {
        // show the icon of the app in the side bar of the scene
        app.activate();
    }
    public void showInMainScreen(App app) {
		if (app == currentApp) {
			app.hide ();
			currentApp = null;
		} else {
			hideCurrentApp ();
			currentApp = app;
			app.show ();
		}
    }
    public void hideCurrentApp() {
        if (currentApp != null) {
            currentApp.hide ();
        }
        currentApp = null;
    }

}
