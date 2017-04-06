﻿using UnityEngine;
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
    public int multiplier;

    private GooglePlayController googlePlayController;
    private AdvertisementManager advertisementManager;

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

        GameObject googlePlayObject = GameObject.FindWithTag("GooglePlayController");
        if (googlePlayObject != null) {
            googlePlayController = googlePlayObject.GetComponent<GooglePlayController>();
        } else {
            Debug.LogError("Unable to find GooglePlay object!");
        }

        GameObject advertisementManagerObject = GameObject.FindWithTag("AdvertisementManager");
        if (advertisementManagerObject != null) {
            advertisementManager = advertisementManagerObject.GetComponent<AdvertisementManager>();
        } else {
            Debug.LogError("Unable to find AdvertisementManager object!");
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
        multiplier++;
        score = score + (multiplier * 50);

        // Check for achievements
        // TODO what are all the achievements we have?
        if (score >= 50000) {
            googlePlayController.UnlockAchievement(Achievements.score50000);
        } else if (score >= 10000) {
            googlePlayController.UnlockAchievement(Achievements.score10000);
        } else if (score >= 5000) {
            googlePlayController.UnlockAchievement(Achievements.score5000);
        } else if (score >= 1000) {
            googlePlayController.UnlockAchievement(Achievements.score1000);
        }

        if (multiplier >= 5) {
            googlePlayController.UnlockAchievement(Achievements.multiplier5);
        }
    }

    public void IncreaseKillCount () {
        // TODO maybe only increment achievement every so often, like after you die or after a wave
        googlePlayController.IncrementAchievement(Achievements.kill10);
    }

    public void ResetMultiplier(){
        multiplier = 0;
    }

    public void UploadScore () {
        if (googlePlayController != null) {
            googlePlayController.UploadScore(score);            

            if (score == 0) {
                googlePlayController.UnlockAchievement(Achievements.youDontGetThis);
            }

            googlePlayController.UnlockAchievement(Achievements.dead);
        }
    }

    public void ShowScoreScreen () {
        SceneManager.LoadScene("Score");
        advertisementManager.FinishedRound();
    }
}
