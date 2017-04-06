using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisementManager : MonoBehaviour {

	private int playAdEveryNTimes = 3;
	private int numRoundsPlayed = 1;

	void Awake () {
		DontDestroyOnLoad(gameObject);
	}

	public void FinishedRound () {
		if ((numRoundsPlayed % playAdEveryNTimes) == 0 && Advertisement.IsReady()) {
			Advertisement.Show();
		}

		numRoundsPlayed++;
	}
}