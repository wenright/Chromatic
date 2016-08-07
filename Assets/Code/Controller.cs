using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

	//Singleton
	private static Controller gcsingleton;
	public static Controller Instance { get { return gcsingleton; } }

    // UI controls
    public Slider healthSlider; public float hp; public readonly float  MAX_HP = 200;
    public Image Fill;  // assign in the editor the "Fill"
    private Player player;
	private Color bgcolor;

    void Awake () {
		if (gcsingleton == null)
			gcsingleton = this;
		else
			Destroy (gameObject);
		DontDestroyOnLoad (this.gameObject);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        if (healthSlider) {
            healthSlider.value = hp / MAX_HP;
            Fill.color = player.GetColor();
        }
        if (hp > 0) {
            hp--;
        }
		if (player.GetColor () == ColorList.purple || player.GetColor () == ColorList.orange || player.GetColor () == ColorList.green) {
			ChangeBGColor (player.GetColor ());
		} else
			ChangeBGColor (ColorList.white);
    }
	void ChangeBGColor (Color color) {
		if (color == Color.white)
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().backgroundColor = color / 6;
		else
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().backgroundColor = color / 4;
	}
}
