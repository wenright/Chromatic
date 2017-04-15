using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {
    public int score;
    public int wave;
	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this);
    }
    public void Reset()
    {
        score = 0;
        wave = 0;
    }
}
