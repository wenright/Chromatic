using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GooglePlay {

	private readonly string leaderboardID = "CgkIz8WKy80eEAIQAQ";

	public GooglePlay () {
		#if UNITY_ANDROID

		// Sign user in
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

		// TODO maybe reset the current achievement based playerprefs each time the player is signed in, and update the playerprefs so that they are synced up

		#endif
	}
	
	public void UploadScore (int score) {
		// TODO does reporting a score that is lower than a players highest score actually do anything?
		#if UNITY_ANDROID
		Social.ReportScore(score, leaderboardID, (bool success) => {
			if (success) {
				Debug.Log("Successfully uploaded a high score");
			} else {
				Debug.LogError("Failed uploading high score to leaderboards!");
			}
		});
		#endif
	}

	public void UnlockAchievement (string achievementID) {
		#if UNITY_ANDROID

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
		#endif
	}

	public void IncrementAchievement (string achievementID) {
		#if UNITY_ANDROID

		// TODO cached incremental achievements
		PlayGamesPlatform.Instance.IncrementAchievement(achievementID, 1, (bool success) => {
			if (success) {
				Debug.Log("Successfully unlocked achievement " + achievementID);
			} else {
				Debug.LogError("Failed unlocking achievement!");
			}
		});
		#endif
	}

	public bool IsAchievementUnlocked (string achievementID) {
		return PlayerPrefs.GetInt(achievementID, 0) == 1;
	}

	public void ShowLeaderboardUI () {
		#if UNITY_ANDROID
		PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardID);
		#endif
	}

	public void ShowAchievementsUI () {
		#if UNITY_ANDROID
		Social.ShowAchievementsUI();
		#endif
	}
}
