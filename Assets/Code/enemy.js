//The speed at which the enemy moves towards the move location, should be between 0 and 1
var LERP_SPEED : float;		//A higher number means faster movement

//The speed at which the object rotates around its center
var ROTATE_SPEED : int = 50;

//The color of the enemy, starts out either orange, purple, or green
var color : Color = Color.white;

//The target that the enemy chases, typically the player, but could be anything
private var target : Transform;

//The sprite renderer component of this object
var sprite : SpriteRenderer;

function Start () {
	//Finds the object tagged player
	target = GameObject.FindGameObjectWithTag("Player").transform;		//TODO: getting error messages from player being dead and enemies spawning.

	//For now, this just makes it a random color
	var num : int = Random.Range(1, 4);
	
	switch (num) {
		case 1: color = Color(1, 0.65, 0, 1);		//Orange
			break;
		case 2: color = Color.magenta;
			break;
		case 3: color = Color.green;
			break;
		default: break;
	}
	
	sprite.color = color;
}

function Update () {
	transform.RotateAround(transform.position, transform.forward, ROTATE_SPEED * Time.deltaTime);
	
	//Prevents annoying 2s lag and error messages
	if (target)
		transform.position = Vector2.Lerp(transform.position, target.position, LERP_SPEED);
}