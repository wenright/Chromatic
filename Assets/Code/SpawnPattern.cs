using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//IMPORTANT//
//Vector4 being hacked for a data structure (don't judge)
//x = x cord
//y = y cord
//z = time delay for spawn
//w = color (0 green, 1 orange, 2 purple)
public class SpawnPattern {
    Stack<SpawnCommand> pattern = new Stack<SpawnCommand>();
    // Use this for initialization
    public SpawnPattern(int p)
    {
        //set pattern based on p
		switch (p) {
			case 1: 
				pattern.Push (new SpawnCommand(10, 6, 3, ColorList.green));
				break;
			case 2:
				pattern.Push (new SpawnCommand(10, 6, 0, ColorList.orange));
				pattern.Push (new SpawnCommand(-10, 6, 0, ColorList.purple));
				break;
		}
    }

    // Update is called once per frame
    void Update() {

    }
    //tells you if the pattern is complete
    public bool isComplete()
    {   
        if (pattern.Count == 0)
            return true;
        return false;
    }

    //commented until enemy code is done
    public SpawnCommand getNext(){
        return pattern.Pop();
    }   
}
