#pragma strict

var scoreTexts : GUIText[];

function Start () {
	if (scoreTexts.Length > 4) {
		if (PlayerPrefs.GetInt("highscore1") != 0)
			scoreTexts[0].text = "" + PlayerPrefs.GetInt("highscore1");
		if (PlayerPrefs.GetInt("highscore2") != 0)
			scoreTexts[1].text = "" + PlayerPrefs.GetInt("highscore2");
		if (PlayerPrefs.GetInt("highscore3") != 0)
			scoreTexts[2].text = "" + PlayerPrefs.GetInt("highscore3");
		if (PlayerPrefs.GetInt("highscore4") != 0)
			scoreTexts[3].text = "" + PlayerPrefs.GetInt("highscore4");
		if (PlayerPrefs.GetInt("highscore5") != 0)
			scoreTexts[4].text = "" + PlayerPrefs.GetInt("highscore5");
	}
	else
		print ("Make sure that you correctly assigned the score text variables.");
}

function Update () {
	if (Input.GetKeyDown ("escape"))
		Application.LoadLevel("main");
}