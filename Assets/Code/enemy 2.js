//The spinny version of the enemy
//The speed at which the enemy moves towards the move location, should be between 0 and 1
var MOVE_SPEED : float = 300;		//A higher number means faster movement

//The speed at which the object rotates around its center
var ROTATE_SPEED : int = 50;

private var width : float;
private var height : float;

private var label_x : float;
private var label_y : float;
private var label_width : int = 30;

var enemy_label : Texture2D;
private var show_label : boolean = false;

//The color of the enemy, starts out either orange, purple, or green
var color : Color = Color.white;
private var purple : Color = Color(191/255.0F, 0, 1, 1);
private var green : Color = Color(0, 1, 0, 1);
private var orange : Color = Color(1, 127/255.0F, 0, 1);

//The target that the enemy chases, typically the player, but could be anything
private var target : Transform;

//The sprite renderer component of this object
var sprite : SpriteRenderer;

function Start () {
	//Finds the object tagged player
	target = GameObject.FindGameObjectWithTag("Player").transform;		//TODO: getting error messages from player being dead and enemies spawning.

	//For now, this just makes it a random color
	var num : int = Random.Range(1, 4);
	
	SetColor(num);
	
	sprite.color = color;
	
	width = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).x;
	height = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).y;
}

function OnGUI () {
	if (show_label)
		GUI.Label(Rect(label_x, label_y, label_width, label_width), enemy_label);
}

function Update () {
	//Move/rotate enemy
	if (target) {
		transform.LookAt(transform.position + Vector3(0,0,1), target.position - transform.position);
		rigidbody2D.AddForce(transform.up * Time.deltaTime * MOVE_SPEED);
	}
	MOVE_SPEED += Time.deltaTime * 25;
	transform.position.z = 0;

	//draw a GUI icon indicating incoming enemy if off screen
	show_label = (Mathf.Abs(transform.position.x) > width || Mathf.Abs(transform.position.y) > height);
	
	if (transform.position.x > width)
		label_x = Screen.width - label_width;
	else if (transform.position.x < -width)
		label_x = 0;
	else 
		label_x = Camera.main.WorldToScreenPoint(transform.position).x;
	
	if (transform.position.y > height)
		label_y = 0;
	else if (transform.position.y < -height)
		label_y = Screen.height - label_width;
	else
		label_y = Screen.height - Camera.main.WorldToScreenPoint(transform.position).y;
		
	//Rotate the square
	transform.GetChild(0).rotation.z += Time.deltaTime;
}

function SetColor (num : int) {
	switch (num) {
		case 1: color = orange;
			break;
		case 2: color = purple;
			break;
		case 3: color = green;
			break;
		default: break;
	}
}