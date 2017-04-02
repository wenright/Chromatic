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
		Social.ReportScore(score, leaderboardID, (bool success) => {
			if (success) {
				Debug.Log("Successfully uploaded a high score");
			} else {
				Debug.LogError("Failed uploading high score to leaderboards!");
			}
		});
	}

	public void UnlockAchievement (string achievementID) {
		// TODO cache achievments locally so they aren't constantly asking the server if they have been completed
		Social.ReportProgress(achievementID, 100.0f, (bool success) => {
			if (success) {
				Debug.Log("Successfully unlocked achievement " + achievementID);
			} else {
				Debug.LogError("Failed unlocking achievement!");
			}
		});
	}

	public void IncrementAchievement (string achievementID) {
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
