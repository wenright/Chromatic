//The color of the enemy, starts out either orange, purple, or green
private var color : Color = Color.white;

function Start () {
	//For now, this just makes it a random color
	var num : int = Mathf.range(1, 3);
	
	switch (num) {
		case 1: color = Color(1, 0.65, 0, 1);		//Orange
			break;
		case 2: color = Color.magenta;
			break;
		case 3: color = Color.green;
			break;
		default: break;
	}
}

function Update () {

}