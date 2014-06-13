//The color of the enemy, starts out either orange, purple, or green
var color : Color = Color.white;

//The sprite renderer component of this object
var sprite : SpriteRenderer;

function Start () {
	sprite = GetComponent(SpriteRenderer);

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
	
}