using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {
    public int score;
    public int wave;
    private static ScoreCounter scsingleton;
    // Use this for initialization
    void Awake () {
        if (scsingleton == null)
            scsingleton = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }
    public void Reset()
    {
        score = 0;
        wave = 0;
    }
}
