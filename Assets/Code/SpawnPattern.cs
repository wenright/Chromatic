using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPattern : MonoBehaviour {
    Stack<Vector2> pattern = new Stack<Vector2>();
    // Use this for initialization
    public SpawnPattern(int p)
    {
        //set pattern based on p
    }

    // Update is called once per frame
    void Update() {

    }
    //tells you if the pattern is complete
    bool isComplete()
    {
        
        if (pattern.Count == 0)
            return true;
        else return false;
    }

    //commented until enemy code is done
    //Enemy getNext(){
        //pattern.Pop();
    //}   
}
