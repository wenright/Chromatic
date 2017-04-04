using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {
	
	public Text wave;

	private bool hasPressedScreen = true;

	// Use this for initialization
	void Start () {
		wave.text = "0";

		// Make sure user releases press before tapping again, so it doesn't go immediately back to game
		if (Input.touches.Length > 0) {
			hasPressedScreen = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touches.Length <= 0) {
			hasPressedScreen = true;
		}

		if (hasPressedScreen && Input.GetMouseButtonUp(0)) {
			SceneManager.LoadScene("Menu");
		}
	}
}
