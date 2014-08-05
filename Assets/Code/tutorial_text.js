﻿var text : GUIText;
var tut_triangle : GameObject;
var tut_square : GameObject;
var done : boolean;
var player : player_controller;

function Start () {
	done = false;
}

function Update () {
	if ((Input.touchCount > 0 || Input.GetButtonDown("Fire1") && !done))
		do_a_thing();
}

function do_a_thing () {
	done = true;		//this tut goes too fast, gotta fix the player so they stay the color longer just for the tutorial
	
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
	text.text = "Nice! Returning to main menu!";
	yield WaitForSeconds(2.5);
	text.text = "Remember, get combos to rack up more points";
	yield WaitForSeconds(3);
	Application.LoadLevel("main");
	
}