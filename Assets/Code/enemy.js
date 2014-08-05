//The speed at which the enemy moves towards the move location, should be between 0 and 1
var MOVE_SPEED : float = Random.Range(250,350);		//A higher number means faster movement

//The speed at which the object rotates around its center
var ROTATE_SPEED : float = 50;

private var width : float;
private var height : float;

private var label_x : float;
private var label_y : float;
private var label_width : int = Screen.width/20;

var enemy_label : Texture2D;
private var show_label : boolean = false;

//The color of the enemy, starts out either orange, purple, or green
var color : Color = Color.white; 

//The target that the enemy chases, typically the player, but could be anything
private var target : Transform;

//The sprite renderer component of this object
var sprite : SpriteRenderer;

function Start () {
	//Finds the object tagged player
	target = GameObject.FindGameObjectWithTag("Player").transform;
	
	width = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).x;		//TODO: Seems a bit unnecessary to recalculate this every time an enemy spawns
	height = Camera.main.ViewportToWorldPoint(Vector3(1, 1, 10)).y;
}

function OnGUI () {
	if (show_label)
		GUI.Label(Rect(label_x, label_y, label_width, label_width), enemy_label);
}

function Update () {
	//Move/rotate enemy
	if (target) {
		//transform.LookAt(transform.position + Vector3(0,0,1), target.position - transform.position);
		var newRotation = Quaternion.LookRotation(transform.position - target.transform.position, Vector3.forward);
		newRotation.x = 0.0;
		newRotation.y = 0.0;
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, ROTATE_SPEED * Time.deltaTime);

		rigidbody2D.AddForce(transform.up * Time.deltaTime * MOVE_SPEED);
	}
	MOVE_SPEED += Time.deltaTime * 55;
	ROTATE_SPEED += Time.deltaTime * 0.32;
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
}

function SetColor (num : int) {
	switch (num) {
		case 1: color = player_controller.orange;
			break;
		case 2: color = player_controller.purple;
			break;
		case 3: color = player_controller.green;
			break;
		default: break;
	}
	
	sprite.color = color;
}

function SetColor (colorIn : Color){
	color = colorIn;
}