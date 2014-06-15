var wait_time : int = 1;

function Start () {
	yield WaitForSeconds(wait_time);
	Destroy (gameObject);
}