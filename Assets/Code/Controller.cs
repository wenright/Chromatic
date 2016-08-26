using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {

	//Singleton
	private static Controller gcsingleton;
	public static Controller Instance { get { return gcsingleton; } }

    // UI controls
    public Slider healthSlider; public float hp; public readonly float  MAX_HP = 200;
	public Text waveText;
	public Text scoreText;

    public Image Fill;  // assign in the editor the "Fill"
    private Player player;
	private Color bgcolor;

    public int level;
	public int wavecounter;

	public int score;
	public int multipler;


    void Awake () {
		wavecounter = 0;
		if (gcsingleton == null) {
			gcsingleton = this;
		}
		else {
			Destroy (gameObject);
		}
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
		waveText.text = "Wave " + wavecounter;
		scoreText.text = score.ToString ();
		if (healthSlider) {
			healthSlider.value = hp / MAX_HP;
			Fill.color = player.GetColor ();
		}
        if (hp > 0) {
            hp--;
        }

		if (player.GetColor () == ColorList.purple || player.GetColor () == ColorList.orange || player.GetColor () == ColorList.green) {
			ChangeBGColor (player.GetColor ());
		} else {
			ChangeBGColor (ColorList.white);
		}
    }
	void ChangeBGColor (Color color) {
		if (color == Color.white)
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().backgroundColor = color / 6;
		else
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().backgroundColor = color / 4;
	}
	public void SetLevel(int i){
		if (i != level) {
			wavecounter++;
			level = i;
		}
	}
	public void IncreaseScore(){
		multipler++;
		score = score + (multipler * 50);
	}

	public void RestMultiplier(){
		multipler = 0;
	}
}
