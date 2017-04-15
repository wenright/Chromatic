using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GooglePlay {

	private readonly string leaderboardID = "CgkIz8WKy80eEAIQAQ";

	public GooglePlay () {
		if (!Social.localUser.authenticated) {
			PlayGamesPlatform.DebugLogEnabled = true;
			PlayGamesPlatform.Activate();
			
			PlayGamesPlatform.Instance.Authenticate((bool success) => {
				if (success) {
					Debug.Log("Successfully signed in");
				} else {
					Debug.LogError("Failed signing in!");
				}
			});
		}
	}
	
	public void UploadScore (int score) {
		// TODO does reporting a score that is lower than a players highest score actually do anything?
		Social.ReportScore(score, leaderboardID, (bool success) => {
			if (success) {
				Debug.Log("Successfully uploaded a high score");
			} else {
				Debug.LogError("Failed uploading high score to leaderboards!");
			}
		});
	}

	public void UnlockAchievement (string achievementID) {
		// Do nothing if this achievement has already been completed
		if (PlayerPrefs.HasKey(achievementID)) {
			return;
		}

		Social.ReportProgress(achievementID, 100.0f, (bool success) => {
			if (success) {
				Debug.Log("Successfully unlocked achievement " + achievementID);
				PlayerPrefs.SetInt(achievementID, 1);
			} else {
				Debug.LogError("Failed unlocking achievement!");
			}
		});
	}

	public void IncrementAchievement (string achievementID) {
		// TODO cached incremental achievements
		PlayGamesPlatform.Instance.IncrementAchievement(achievementID, 1, (bool success) => {
			if (success) {
				Debug.Log("Successfully unlocked achievement " + achievementID);
			} else {
				Debug.LogError("Failed unlocking achievement!");
			}
		});
	}

	public void ShowLeaderboardUI () {
		PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardID);
	}

	public void ShowAchievementsUI () {
		Social.ShowAchievementsUI();
	}
}
