using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : App {

	[SerializeField]
	private GameObject input;
	[SerializeField]
	private InputField inputfield;
	[SerializeField]
	private Text screen;
	[SerializeField]
	private int now_line = 0;

	// Use this for initialization
	void Start () {
		base.Start ();
		input = transform.Find ("Terminal_window").gameObject;
		inputfield = input.GetComponentInChildren<InputField> ();
		screen = GameObject.Find ("screen").GetComponent <UnityEngine.UI.Text>();
	}
		
	// Update is called once per frame
	void Update () {
		if (inputfield.isFocused && inputfield.text != "" && Input.GetKey (KeyCode.Return)) {
			get_command (inputfield.text);
			refresh_inputfield ();
		}
	}

	void get_command(string command){
		if (command != "\n") {
			string echo = command_handler (command);
			screen.text = screen.text + "user@hack:-$ " + command + echo + "\n";
			if (now_line >= 12) {
				int delete_line_num = now_line - 12;
				for (int i = 0; i < delete_line_num; i++) {
					screen.text = screen.text.Substring (screen.text.IndexOf('\n')+1);
				}
				now_line = now_line - delete_line_num;
			}
		}
	}
	//just refresh InputField text
	void refresh_inputfield()
	{
		inputfield.text = "";
	}
	//handle all command
	string command_handler(string command){
		string echo;
		if (command == "help\n") 
		{
			echo = "Common:\n" +
				"\t\thelp -list all command and some information.\n" +
				"\t\texit -just exit.\n" +
				"\t\tEvelator:\n" +
				"\t\tgoto floor #floor_num\n" +
				"mail list:\n" +
				"\t\tmail open ;mailname\n" +
				"camera system\n" +
				"\t\tcamera on\n" +
				"\t\tls\n" +
				"\t\topen ;filename";
			now_line = now_line + 12;
		}
		else if(command == "camera on\n")
		{
			echo = "Camera is on now.";
			now_line = now_line + 2;
		}
		else
		{
			echo = "Command does not exist.";
			now_line = now_line + 2;
		}
		return echo;
	}
}
