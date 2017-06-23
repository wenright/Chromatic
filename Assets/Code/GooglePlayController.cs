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
		// Do nothing if this achievement has already been completed
		if (PlayerPrefs.HasKey(achievementID)) {
			return;
		}

		PlayerPrefs.SetInt(achievementID, 1);
		

		googlePlay.UnlockAchievement(achievementID);
	}

	public void IncrementAchievement (string achievementID) {
		googlePlay.IncrementAchievement(achievementID);
	}

	public bool IsAchievementUnlocked (string achievementID) {
		return googlePlay.IsAchievementUnlocked(achievementID);
	}

	public void ShowAchievementsUI () {
		googlePlay.ShowAchievementsUI();
	}
}
