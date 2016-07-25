using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPattern {

    Stack<SpawnCommand> pattern = new Stack<SpawnCommand>();
	bool wait = false;

    // Use this for initialization
    public SpawnPattern(int p) {
        //set pattern based on p
		switch (p) {
			case 1: 
				pattern.Push (new SpawnCommand (10, 6, 3, ColorList.green));
				wait = true;
				break;
			case 2:
				pattern.Push (new SpawnCommand (10, 6, 0, ColorList.orange));
				pattern.Push (new SpawnCommand (-10, 6, 0, ColorList.purple));
				wait = true;
				break;
			case 3:
				pattern.Push (new SpawnCommand (12, 6, 3, ColorList.orange));
				pattern.Push (new SpawnCommand (10, 0, 0, ColorList.orange));
				pattern.Push (new SpawnCommand (12, -6, 0, ColorList.orange));
				pattern.Push (new SpawnCommand (-12, 6, 3, ColorList.purple));
				pattern.Push (new SpawnCommand (-10, 0, 0, ColorList.purple));
				pattern.Push (new SpawnCommand (-12, -6, 0, ColorList.purple));
				wait = true;
				break;
			case 4:
				pattern.Push (new SpawnCommand (12, 6, 1, ColorList.green));
				pattern.Push (new SpawnCommand (12, -6, 0, ColorList.green));
				pattern.Push (new SpawnCommand (-12, 6, 1, ColorList.green));
				pattern.Push (new SpawnCommand (-12, -6, 0, ColorList.green));
				wait = true;
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
	public bool isWaiting(){
		return wait;
	}

    //commented until enemy code is done
    public SpawnCommand getNext(){
        return pattern.Pop();
    }   
}
