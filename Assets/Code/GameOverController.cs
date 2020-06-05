using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {
	
	public Text wave;
    public Text score;
    public Text youmadeit;
    Gradient g = new Gradient();
    float t;
    int pos;
    public string[] msgs =
    {
        "You made it to wave:",
        "Wow! You made it to wave:",
        "YOU HAVE FAILED ON WAVE:",
        "You dun goofed on wave:",
        "You have disappointed us on wave:",
        "Proud of you! Wave:",
        "ded on wave:",
        "You died on wave:",
        "You will never pass wave:",
        "You peaked on wave:"

    };
   
	// Use this for initialization
	void Start () {

        try
        {
            wave.text = GameObject.FindWithTag("ScoreCounter").GetComponent<ScoreCounter>().wave.ToString();
            score.text = GameObject.FindWithTag("ScoreCounter").GetComponent<ScoreCounter>().score.ToString();
        } catch (System.NullReferenceException e)
        {
            wave.text = "-1";
            score.text = "-1";

        }
        int rand = Random.Range(0, 10);
        youmadeit.text = msgs[rand];
        
        GradientColorKey blackkey = new GradientColorKey(Color.black, 0);
        GradientColorKey greenkey = new GradientColorKey(ColorList.green, 0.25f);
        GradientColorKey purplekey = new GradientColorKey(ColorList.purple, 0.50f);
        GradientColorKey orangekey = new GradientColorKey(ColorList.orange, 0.6f);
        GradientColorKey blackkeyend = new GradientColorKey(Color.black, 0.8f);
        GradientColorKey[] keyarray = { blackkey, greenkey, purplekey, orangekey, blackkeyend };
        GradientAlphaKey[] alpharray = { new GradientAlphaKey(1, 0) };
   
        g.SetKeys(keyarray, alpharray);
        Time.timeScale = 0.1f;
	}

	void Update () {
        t += Time.deltaTime;
        if (t >= 1f)
            t = 0;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().backgroundColor = g.Evaluate(t);

    }
}
