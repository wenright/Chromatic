var player_name : String;
var ask_for_name : boolean = false;
var gui_skin : GUISkin;
var status_text : String;
var url : String = "http://wenright.github.io/";

function OnGUI () {
	if (ask_for_name) {
		GUI.skin = gui_skin;
	
		if (PlayerPrefs.GetInt("first_time") == 0) {
			GUI.Box(Rect(0, 0, Screen.width, Screen.height), "Looks like this is your first time, want to play the tutorial?");
		
			if (GUI.Button(Rect((Screen.width / 2) + 50, Screen.height / 2, 50, 30), "Yes")) {
				Application.LoadLevel(1);
				PlayerPrefs.SetInt("first_time", 1);
			}
			
			if (GUI.Button(Rect((Screen.width / 2) - 50, Screen.height / 2, 50, 30), "No"))
				PlayerPrefs.SetInt("first_time", 1);
		}
		else {
			if (GUI.Button(Rect((Screen.width / 2) - 30, 50, 60, 30), "Play")) {
				PlayerPrefs.SetString("Name", player_name);
				Application.LoadLevel(2);
			}
		}
		
		player_name = GUI.TextField(Rect((Screen.width / 2) - 60, 120, 120, 75), player_name);
	}
	
	GUI.Label(Rect(10, Screen.height - 30, Screen.width, 60), status_text);
}

function Start () {
	player_name = PlayerPrefs.GetString("Name");
	ask_for_name = false;
	
	status_text = "";
	var site : WWW = new WWW(url);
	yield site;
	status_text = site.text.Substring(27, 50);	//Make all status messages 50 characters or shorter
}

function Update () {
	//TODO REMOVE THIS BEFORE DEPLOYMENT, this is just for debugging
	if (Input.GetKeyDown("r"))
		PlayerPrefs.SetInt("first_time", 0);
}