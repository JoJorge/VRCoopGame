using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcPlayer : MonoBehaviour {

    private GameObject slideBar;
    private GameObject mainScreen;
    private App currentApp;
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
        apps = new List<App>();
        // get all apps
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool isCurrentApp(App app) {
        return app.Equals (currentApp);
    }

    public void send(string type, string content) {
        // send message to VrPlayer
    }
    public void receive<T>(string type, T content) {
        // receive message from VrPlayer
        int idx = type.IndexOf(" ");    
        string typeName = type.Substring(0, idx);
        string header = type.Substring (idx+1);
        foreach (App app in apps) {
            if (app.name == typeName) {
                app.input (header, content);
                break;
            }
        }
    }
    private void showInSideBar(App app) {
        // show the icon of the app in the side bar of the scene
    }
    public void showInMainScreen(App app) {
        hideCurrentApp ();
        currentApp = app;
        app.show ();
    }
    private void hideCurrentApp() {
        if (currentApp != null) {
            currentApp.hide ();
        }
        currentApp = null;
    }

}
