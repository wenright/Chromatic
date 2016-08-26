using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverController : MonoBehaviour {
	public Text wave;
	// Use this for initialization
	void Start () {
		wave.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 || Input.GetMouseButtonUp(0)) {
			SceneManager.LoadScene("Will Test");
		}
	}
}
