var repetitions : int;
var duration : float;
var intensity : float;

private var start_location : Vector3;

function Start () {
	start_location = transform.position;
}

function Shake () {
	for (var i : int = 0; i < repetitions; i++) {
		transform.position = start_location + Vector2(Random.value * intensity, Random.value * intensity);
		yield WaitForSeconds(duration);
	}
	
	transform.position = start_location;
}