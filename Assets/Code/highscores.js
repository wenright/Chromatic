#pragma strict

import SimpleJSON;

var scoreTexts : GUIText[];
var nameTexts : GUIText[];

function Start () {
	var url = "https://api.scoreoid.com/v1/getScores";
	
	var form = new WWWForm();
	
	form.AddField("api_key", "177e5b33f5cad7e6dd40927932dcbd33dd1b4f4e");
	form.AddField("game_id", "5b67916394");
	form.AddField("response", "JSON");
	form.AddField("order_by", "score");
	form.AddField("order", "asc");
	form.AddField("limit", "5");
	
	var www = new WWW(url, form);
	
	yield www;
	
	if (www.error != null)
		print(www.error);
	
	var json = JSON.Parse(www.text);
	for (var i : int = 0; i < 5; i++) {
		var s : String = json[i]["Score"]["score"];
		if (s != null)
			scoreTexts[i].text = s;
		else
			continue;
		
		s = json[i]["Player"]["first_name"];
		if (s != null)
			nameTexts[i].text = s;
		else
			nameTexts[i].text = "Anonymous";
	}
}

function Update () {
	if (Input.GetKeyDown ("escape"))
		Application.LoadLevel("menu");
}