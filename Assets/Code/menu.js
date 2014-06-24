var player_name : String;

function OnGUI () {
	if (PlayerPrefs.GetInt("first_time") == 0) {
		GUI.Box(Rect(0, 0, Screen.width, Screen.height), "Looks like this is your first time, want to play the tutorial?");
	
		if (GUI.Button(Rect((Screen.width / 2) - 50, Screen.height / 2, 50, 30), "Yes")) {
			Application.LoadLevel(1);
			PlayerPrefs.SetInt("first_time", 1);
		}
		
		if (GUI.Button(Rect((Screen.width / 2) + 50, Screen.height / 2, 50, 30), "No"))
			PlayerPrefs.SetInt("first_time", 1);
	}
	else {
		if (GUI.Button(Rect(Screen.width / 2, 50, 60, 30), "Play")) {
			PlayerPrefs.SetString("Name", player_name);
			Application.LoadLevel(2);
		}
	}
	
	player_name = GUI.TextField(Rect(Screen.width / 2, 120, 60, 30), player_name);
}

function Start () {
	player_name = PlayerPrefs.GetString("Name");
}

function Update () {
	//TODO REMOVE THIS BEFORE DEPLOYMENT, this is just for debugging
	if (Input.GetKeyDown("r"))
		PlayerPrefs.SetInt("first_time", 0);
}