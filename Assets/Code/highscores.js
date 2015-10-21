#pragma strict

function Start () {
	getScores();
}

function getScores() {
    var hs_get = WWW("http://128.211.207.196/display.php");
    yield hs_get;
 
    if(hs_get.error) {
    	print("There was an error getting the high score: " + hs_get.error);
    } else {
        gameObject.GetComponent.<GUIText>().text = hs_get.text;
    }
}

function Update () {
	if (Input.GetKeyDown ("escape"))
		Application.LoadLevel("menu");
}