using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour {

	private Leaderboard leaderboard;

	void Start () {
		DontDestroyOnLoad(gameObject);

		leaderboard = new Leaderboard();
	}
	
	public void UploadScore (int score) {
		leaderboard.UploadScore(score);
	}

	public void ShowLeaderboardUI () {
		leaderboard.ShowLeaderboardUI();
	}
}
