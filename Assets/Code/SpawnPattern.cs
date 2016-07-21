using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPattern : MonoBehaviour {
    Stack<Vector3> pattern = new Stack<Vector3>();
    // Use this for initialization
    public SpawnPattern(int p)
    {
        //set pattern based on p
    }

    // Update is called once per frame
    void Update() {

    }
    //tells you if the pattern is complete
    public bool isComplete()
    {   
        if (pattern.Count == 0)
            return true;
        else return false;
    }

    //commented until enemy code is done
    public Vector3 getNext(){
        return pattern.Pop();
    }   
}
