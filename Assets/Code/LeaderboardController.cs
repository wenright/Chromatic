using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class LeaderboardController {

	private readonly string leaderboardID = "CgkIz8WKy80eEAIQAQ";

	// Use this for initialization
	public LeaderboardController () {
		if (!Social.localUser.authenticated) {
			PlayGamesPlatform.DebugLogEnabled = true;
			PlayGamesPlatform.Activate();
			
			PlayGamesPlatform.Instance.Authenticate((bool success) => {
				// handle success or failure
				Debug.Log("name: " + Social.localUser.userName + ", state: " + Social.localUser.state + ", authenticated: " + Social.localUser.authenticated + ", id: " + Social.localUser.id);

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
			// handle success or failure
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
