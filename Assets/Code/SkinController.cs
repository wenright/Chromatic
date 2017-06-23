using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour {

    public Sprite currentsprite;
    public Sprite skull;
    public Sprite square;
    public Sprite circle;
    public Sprite star;

    public enum Skins:int {
    	skull = 1,
    	square = 2,
    	circle = 3,
    	star = 4
    };

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Sprite GetCurrentSprite()
    {
        return currentsprite;
    }
}
