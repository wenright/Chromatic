using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlayController : MonoBehaviour {

	private GooglePlay googlePlay;

	void Start () {
		DontDestroyOnLoad(gameObject);

		googlePlay = new GooglePlay();
	}
	
	public void UploadScore (int score) {
		googlePlay.UploadScore(score);
	}

	public void ShowLeaderboardUI () {
		googlePlay.ShowLeaderboardUI();
	}

	public void UnlockAchievement (string achievementID) {
		googlePlay.UnlockAchievement(achievementID);
	}

	public void IncrementAchievement (string achievementID) {
		googlePlay.IncrementAchievement(achievementID);
	}

	public void ShowAchievementsUI () {
		googlePlay.ShowAchievementsUI();
	}
}
