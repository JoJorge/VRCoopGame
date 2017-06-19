using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : App {

	[SerializeField]
	private GameObject input;
	[SerializeField]
	private GameObject screen_input;
	[SerializeField]
	private InputField inputfield;
	[SerializeField]
	private Text screen;
	[SerializeField]
	private int now_line = 0;
	[SerializeField]
	private int[] hacklist;
	[SerializeField]
	private int debug;


	// Use this for initialization
    override public void Start() {
        input = transform.Find ("Terminal_window").gameObject;
        inputfield = input.GetComponentInChildren<InputField> ();
		screen = input.GetComponentInChildren<Text> ();
		hacklist = new int[4];
		hacklist [0] = 1;
		hacklist [2] = 1;
        base.Start ();
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
			screen.text = screen.text + "user@hack:-$ " + command + echo;
			if (now_line >= 12) {
				int delete_line_num = now_line - 12;
				for (int i = 0; i < delete_line_num; i++) {
					screen.text = screen.text.Substring (screen.text.IndexOf('\n')+1);
				}
				now_line = now_line - delete_line_num;
			}
			if (echo == "clean\n") {
				screen.text = "";
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
		
		string echo = "";
		string[] help_command;
		int[] help_line;
		help_command = new string[4];
		help_line = new int[4];

		help_command[0] = "Common:\n" +
			"\t\thelp -list all command and some information.\n" +
			"\t\texit -just exit.\n";
		help_command[1] = "\t\tEvelator:\n" +
			"\t\tgoto floor #floor_num\n";
		help_command[2] = "mail list:\n" +
			"\t\tmail open ;mailname\n";
		help_command[3] = "camera system\n" +
			"\t\tcamera on\n" +
			"\t\tls\n" +
			"\t\topen ;filename\n";
		
		help_line [0] = 3;
		help_line [1] = 2;
		help_line [2] = 2;
		help_line [3] = 4;

		if (command == "help\n") {
			int temp_line = 0;
			for(int i=0;i<4;i++){
				if(hacklist[i]==1){
					temp_line = temp_line + help_line[i];
					echo = echo + help_command[i];
				}
			}
			now_line = now_line + temp_line + 1;
		} else if (command == "camera on\n") {
			send ("camera", "on");
			echo = "camera is on now.\n";
			now_line = now_line + 2;
		} else if ((debug = command.IndexOf ("goto floor"))==0) 
		{
			var parts = command.Split (' ');
			if (parts.Length == 3) {
				parts [2] = parts [2].Remove (parts [2].Length - 1);
				echo ="Evelator goto floor "+parts[2]+" successfully";
				send ("elevator", "floor " + parts [2]);
			} else {
				echo = "Evelator command: goto floor #floor_num\n";
			}
			now_line = now_line + 2;
		}
		else if (command == "cls\n") {
			now_line = 0;
			echo = "clean\n";
		}
		else
		{
			echo = "Command does not exist.\n";
			now_line = now_line + 2;
		}
		return echo;
	}

	protected override void output (string header, string content)
	{
		base.output (header, content);
		var parts = content.Split (' ');
        if (parts [0] == "hack") {
			screen.text = screen.text + "Congraduation! VrPlayer has hacked " + parts[1] +" !\n";
			if (parts [1] == "elevator") {
				hacklist [1] = 1;
            }
			if (parts [1] == "camera_system") {
				hacklist [3] = 1;
			}
		}
	}
}
