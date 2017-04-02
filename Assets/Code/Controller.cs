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

    private LeaderboardController leaderboardController;

    void Awake () {
        wavecounter = 0;

        if (gcsingleton == null) {
            gcsingleton = this;
        } else {
            Destroy (gameObject);
        }

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null) {
            player = playerObject.GetComponent<Player>();
        } else {
            Debug.LogError("Unable to find Player object!");
        }

        GameObject leaderboardObject = GameObject.FindWithTag("LeaderboardController");
        if (leaderboardObject != null) {
            leaderboardController = leaderboardObject.GetComponent<LeaderboardController>();
        } else {
            Debug.LogError("Unable to find Leaderboard object!");
        }
    }
    
    void Update () {
        waveText.text = "Wave " + wavecounter;

        if (scoreText != null) {
            scoreText.text = score.ToString ();
        }

        if (healthSlider) {
            healthSlider.value = hp / MAX_HP;
            Fill.color = player.GetColor ();
        }

        if (hp > 0) {
            hp--;
        }

        if (player != null) {
            if (player.GetColor () == ColorList.purple || player.GetColor () == ColorList.orange || player.GetColor () == ColorList.green) {
                ChangeBGColor (player.GetColor ());
            } else {
                ChangeBGColor (ColorList.white);
            }
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

    public void ResetMultiplier(){
        multipler = 0;
    }

    public void UploadScore () {
        if (leaderboardController != null) {
            leaderboardController.UploadScore(score);            
        }
    }
}
