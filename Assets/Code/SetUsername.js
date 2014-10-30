#pragma strict

var fader : fade_in;
var m : menu;
var showInput : boolean = false;
var username : String;
var skin : GUISkin;

function OnGUI () {
	if (showInput) {
		GUI.skin = skin;
		GUI.Label(Rect ((Screen.width / 2) - 100, (Screen.height / 2) - 125, 200, 100), "Enter a name for the leaderboards");
		GUI.SetNextControlName ("name");
		username = GUI.TextField (Rect ((Screen.width / 2) - 100, (Screen.height / 2) - 25, 200, 25), username, 25);
		GUI.FocusControl ("name");
		if (Event.current.Equals(Event.KeyboardEvent("None")) || GUI.Button(Rect((Screen.width / 2) + 10, (Screen.height / 2) + 50, 100, 50), "OK")) {
			if (username != "") {
				//Send message to GlobalHighscores to set username
				PlayerPrefs.SetInt("hasUsername", 1);
				setPlayerName(username);
				showInput = false;
				m.canPlay = true;
				fader.FadeIn();
			}
		}
		else if (GUI.Button(Rect((Screen.width / 2) - 110, (Screen.height / 2) + 50, 100, 50), "Cancel")) {
			PlayerPrefs.SetInt("hasUsername", 2);
			showInput = false;
			m.canPlay = true;
			fader.FadeIn();
		}
	}
}

function Start () {
	if (!fader || !m)
		print("Set fader and menu in SetUsername.js!");
		
	if (PlayerPrefs.GetInt("hasScoreAccount") == 0)
		createPlayer();

	//Player doesn't have a username, or it got reset from an update
	if (PlayerPrefs.GetInt("hasUsername") == 0) {		
		//Fade to black
		yield WaitForSeconds(1);
		fader.FadeOut();
		
		//Prompt for input
		showInput = true;		
	}
	else {
		m.canPlay = true;
	}
}

function createPlayer () {
	//URL to send API request to
	var url = "https://api.scoreoid.com/v1/createPlayer";
	var form = new WWWForm();
	
	form.AddField("api_key", "177e5b33f5cad7e6dd40927932dcbd33dd1b4f4e");
	form.AddField("game_id", "5b67916394");
	form.AddField("response", "json");
	form.AddField("username", SystemInfo.deviceUniqueIdentifier);
	
	var www = new WWW(url, form);
	
	yield www;
	
	if (www.error == null)
		print(www.text);
	else
		print(www.error);
		
	PlayerPrefs.SetInt("hasScoreAccount", 1);
}

function setPlayerName (name : String) {
	//URL to send API request to
	var url = "https://api.scoreoid.com/v1/updatePlayerField";
	var form = new WWWForm();
	
	form.AddField("api_key", "177e5b33f5cad7e6dd40927932dcbd33dd1b4f4e");
	form.AddField("game_id", "5b67916394");
	form.AddField("response", "json");
	form.AddField("username", SystemInfo.deviceUniqueIdentifier);
	form.AddField("field", "first_name");
	form.AddField("value", name);
	
	var www = new WWW(url, form);
	
	yield www;
	
	if (www.error == null)
		print(www.text);
	else
		print(www.error);
}