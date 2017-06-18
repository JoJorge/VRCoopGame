using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class App : MonoBehaviour {

    [SerializeField]
    private GameObject icon;
	[SerializeField]
    private GameObject window;
    private PcPlayer pcPlayer;
    private bool activated;
    private Queue<Data> outputBuffer;

	// Use this for initialization
    public virtual void Start () {
        pcPlayer = PcPlayer.getInstance ();
        outputBuffer = new Queue<Data> ();
		icon = transform.Find (name + "_icon").gameObject;
		window = transform.Find (name + "_window").gameObject;
        icon.SetActive (false);
        hide();
	}

    public bool isActivated() {
        return activated;
    }
    public void input<T> (string header, T content) {
        outputBuffer.Enqueue(DataGenerator.generateDataByContent (header, content));
        if (pcPlayer.isCurrentApp (this)) {
            output ();
        }
    }
    protected void output() {
        while (outputBuffer.Count > 0) {
            Data data = outputBuffer.Dequeue ();
            if (data.getType () == typeof(string)) {
                output (data.getHeader (), (string)data.getContent());
            }
            else if (data.getType() == typeof(Sprite)) {
                output (data.getHeader (), (Sprite)data.getContent());
            }
        }
    }
    protected virtual void output (string header, string content){
    }
    protected virtual void output (string header, Sprite content){
    }
    protected void send(string type, string content) {
        pcPlayer.send (type, content);
    }
    public void activate() {
        // actievate(initialize) the app
        activated = true;
        icon.SetActive (true);
        notified ();
    }
    public void clicked() {
        if (pcPlayer.isCurrentApp (this)) {
            pcPlayer.hideCurrentApp ();
        }
        else {
            pcPlayer.showInMainScreen (this);
        }
    }
    public void notified() {
        // notify the app with important message
    }
    public void show() {
		window.SetActive (true);
		output ();
	}
    public void hide() {
		window.SetActive (false);
    }
}
