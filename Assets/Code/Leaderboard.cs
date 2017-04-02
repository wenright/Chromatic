using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Leaderboard {

	private readonly string leaderboardID = "CgkIz8WKy80eEAIQAQ";

	public Leaderboard () {
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

	public void ShowLeaderboardUI () {
		PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardID);
	}
}
