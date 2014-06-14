var text : GUIText;
var tut_triangle : GameObject;
var tut_square : GameObject;
var done : boolean;

function Start () {
	done = false;
}

function Update () {
	if ((Input.touchCount > 0 || Input.GetButtonDown("Fire1") && !done))
		do_a_thing();
}

function do_a_thing () {
	done = true;		//this tut goes too fast, gotta fix the player so they stay the color longer just for the tutorial
	text.text = "Nice job! You did it! Wow!";
	
	yield WaitForSeconds(2);
	
	text.text = "This triangle is your friend.  Pick it up to change your color.";
	var t1 : GameObject = Instantiate(tut_triangle, Vector3(3, 2, 0), transform.rotation);
	t1.GetComponent(friendly).SetColor(0);	//Red
	
	yield WaitForSeconds(2);
	text.text = "Now pick up a second color to change into a scondary color.";
	var t2 : GameObject = Instantiate(tut_triangle, Vector3(-2, 1.5, 0), transform.rotation);
	t2.GetComponent(friendly).SetColor(2);	//blue
	
	yield WaitForSeconds(2);
	text.text = "Now run into an enemy of that color to destroy it.";
	
	var s1 : GameObject = Instantiate(tut_square, Vector3(0, 2, 0), transform.rotation);
	s1.GetComponent(enemy).SetColor(2);
}