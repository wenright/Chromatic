var text : GUIText;
var tut_triangle : GameObject;
var tut_square : GameObject;
var done : boolean;
var player : player_controller;
var fader : fade_in;

function Start () {
	done = false;
}

function Update () {
<<<<<<< HEAD
	if (!done && (Input.touchCount > 0 || Input.GetButton("Fire1")))
=======
	if ((Input.touchCount > 0 || Input.GetButtonDown("Fire1") && !done)){
		done = true;
>>>>>>> origin/master
		do_a_thing();
	}
}

function do_a_thing () {
	done = true;
	fader.Fade ();
<<<<<<< HEAD
	
=======
			
>>>>>>> origin/master
	text.text = "Move your finger near the circle to move!";
	var prevPos = player.transform.position;
    var actualPos = player.transform.position;
    while(prevPos == actualPos){
   		prevPos = player.transform.position;
   		yield WaitForSeconds(0.5);
   		actualPos = player.transform.position;
  	}
	text.text = "Nice job!";
	
	yield WaitForSeconds(2);
	
	text.text = "This triangle is your friend.\nPick it up to change your color.";
	var t1 : GameObject = Instantiate(tut_triangle, Vector3(3, 2, 0), transform.rotation);
	t1.GetComponent(friendly).SetColor(0);	//Red
	while(player.color != player.red){ 
		yield WaitForSeconds(0.5);
	}
	text.text = "Now pick up a second color to\nchange into a scondary color.";
	var t2 : GameObject = Instantiate(tut_triangle, Vector3(-2, 1.5, 0), transform.rotation);
	t2.GetComponent(friendly).SetColor(2);	//blue
	while(player.color != player.purple){
		yield WaitForSeconds(0.5);
	}
	text.text = "Now run into an enemy\nof that color to destroy it.";
	
	var s1 : GameObject = Instantiate(tut_square, Vector3(0, 5, 0), transform.rotation);
	s1.GetComponent(enemy).SetColor(2);
	while(player.getMultiplier() < 2){
		yield WaitForSeconds(0.5);
	}
	text.text = "Nice! Get combos to rack up more points";
	yield WaitForSeconds(2.5);
	text.text = "Returning to menu!";
	yield WaitForSeconds(3);
	Application.LoadLevel("main");
	
}