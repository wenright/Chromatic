using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    Gradient g;
    float t;

	// Use this for initialization
	void Start () {

   
        GradientColorKey greenkey = new GradientColorKey(ColorList.green, 0.0f);
        GradientColorKey purplekey = new GradientColorKey(ColorList.purple, 0.33f);
        GradientColorKey orangekey = new GradientColorKey(ColorList.orange, 0.66f);
        GradientColorKey[] keyarray = { greenkey, purplekey, orangekey};
        GradientAlphaKey[] alpharray = { new GradientAlphaKey(1, 0) };

        g.SetKeys(keyarray, alpharray);
    }
	
	// Update is called once per frame
	void Update () {

        t += Time.deltaTime;
        // if (t >= 1f)
        //   pos = -1;
        // if (t <= 0)
        //     pos = 1;
        if (t >= 1f)
            t = 0;


        this.gameObject.GetComponent<ParticleSystem>().startColor = g.Evaluate(t);
    }
}
