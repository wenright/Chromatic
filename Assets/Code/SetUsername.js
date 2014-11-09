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
		username = GUI.TextField (Rect ((Screen.width / 2) - 100, (Screen.height / 2) - 25, 200, 25), username, 12);
		GUI.FocusControl ("name");
		if (Event.current.Equals(Event.KeyboardEvent("None")) || GUI.Button(Rect((Screen.width / 2) + 10, (Screen.height / 2) + 50, 100, 50), "OK")) {
			if (username != "") {
				//Send message to GlobalHighscores to set username
				PlayerPrefs.SetInt("hasUsername", 1);
				if (username.Length > 13)
					username = username.Substring(0, 12);
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

	//Player doesn't have a username, or it got reset from an update
	if (PlayerPrefs.GetInt("hasUsername") == 0) {		
		//Fade to black
		yield WaitForSeconds(0.85);
		fader.FadeOut();
		
		//Prompt for input
		showInput = true;		
	}
	else {
		m.canPlay = true;
	}
}

function createPlayer () {		
	PlayerPrefs.SetInt("hasScoreAccount", 1);
}

function setPlayerName (name : String) {
	PlayerPrefs.SetString("username", name);
}