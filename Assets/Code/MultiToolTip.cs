using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiToolTip : MonoBehaviour {
    float maxSize = 0.1f;
    float growthRate = 0.3f;
    float scale = 0f;
    bool alive = true;
    protected Controller gc;
    // Use this for initialization
    void Start () {
        gc = GameObject.FindWithTag("GameController").GetComponent<Controller>();
        this.GetComponent<TextMesh>().text = "X"+gc.multipler.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (alive)
        {
            transform.localScale = Vector3.one * scale;
            scale += growthRate * Time.deltaTime;
            if (scale > maxSize) alive = false;
        }
        else
        {
            transform.localScale = Vector3.one * scale;
            scale -= growthRate * Time.deltaTime;
            if (scale < 0)
                Destroy(gameObject);
        }
    }
}
