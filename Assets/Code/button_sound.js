#pragma strict
var sound : AudioClip;

function Start () {
	DontDestroyOnLoad(this.gameObject);
}

function Play(){
	audio.PlayOneShot (sound);
}

