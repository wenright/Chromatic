using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPattern {

    Stack<SpawnCommand> pattern = new Stack<SpawnCommand>();
	bool wait = false;
	Color a;
	Color b;
	Color c;

    // Use this for initialization
	public SpawnPattern(int p, int scheme) {
		if (scheme == 1) {
			a = ColorList.orange;
			b = ColorList.purple;
			c = ColorList.green;
		} else if (scheme == 2) {
			b = ColorList.orange;
			a = ColorList.purple;
			c = ColorList.green;
		} else {
			c = ColorList.orange;
			b = ColorList.purple;
			a = ColorList.green;
		}

        //set pattern based on p
		switch (p) {
			case 1: 
				pattern.Push (new SpawnCommand (10, 6, 3, a));
				wait = true;
				break;
			case 2:
				pattern.Push (new SpawnCommand (10, 6, 0, b));
				pattern.Push (new SpawnCommand (-10, 6, 0, c));
				wait = true;
				break;
			case 3:
				pattern.Push (new SpawnCommand (12, 6, 3, b));
				pattern.Push (new SpawnCommand (12, 0, 0, b));
				pattern.Push (new SpawnCommand (12, -6, 0, b));
                wait = true;
                break;
            case 4:
				pattern.Push (new SpawnCommand (-12, 6, 3, c));
				pattern.Push (new SpawnCommand (-12, 0, 0, c));
				pattern.Push (new SpawnCommand (-12, -6, 0, c));
                wait = true;
                break;
            case 5:
				pattern.Push (new SpawnCommand (8, -10, 3, a));
				pattern.Push (new SpawnCommand (0, 10, 0, a));
				pattern.Push (new SpawnCommand (-8, -10, 0, a));
				wait = true;
				break;
            case 6:
                //FIXED
                float leveltimer = 0.2f;
                pattern.Push(new SpawnCommand(-10, 10, leveltimer+2f, c, true));
                pattern.Push(new SpawnCommand(-8, 10, leveltimer, c, true));
                pattern.Push(new SpawnCommand(-6, 10, leveltimer, c, true));
                pattern.Push(new SpawnCommand(-4, 10, leveltimer, c, true));
                pattern.Push(new SpawnCommand(-2, 10, leveltimer, c, true));
                pattern.Push(new SpawnCommand(0, 10, leveltimer, c, true));
                pattern.Push(new SpawnCommand(2, 10, leveltimer, b, true));
                pattern.Push(new SpawnCommand(4, 10, leveltimer, c, true));
                pattern.Push(new SpawnCommand(6, 10, leveltimer, b, true));
                pattern.Push(new SpawnCommand(8, 10, leveltimer, c, true));
                pattern.Push(new SpawnCommand(10, 10, leveltimer, b, true));
                wait = false;
                break;
            case 7:
                pattern.Push(new SpawnCommand(-12, 3, 3, a, true));
                pattern.Push(new SpawnCommand(-12, 0, 0, b, true));
                pattern.Push(new SpawnCommand(-12, -3, 0, c, true));
                break;
            case 8:
                leveltimer = 0.2f;
                //FIXED
                pattern.Push(new SpawnCommand(-10, 12, leveltimer+2f, b, true));
                pattern.Push(new SpawnCommand(-8, -12, leveltimer, b, true));
                pattern.Push(new SpawnCommand(-6, 12, leveltimer, b, true));
                pattern.Push(new SpawnCommand(-4, -12, leveltimer, a, true));
                pattern.Push(new SpawnCommand(-2, 12, leveltimer, a, true));
                pattern.Push(new SpawnCommand(0, -12, leveltimer, a, true));
                pattern.Push(new SpawnCommand(2, 12, leveltimer, c, true));
                pattern.Push(new SpawnCommand(4, -12, leveltimer, c, true));
                pattern.Push(new SpawnCommand(6, 12, leveltimer, c, true));
                pattern.Push(new SpawnCommand(8, -12, leveltimer, b, true));
                pattern.Push(new SpawnCommand(10, 12, leveltimer, b, true));
                wait = false;
                break;
            case 9:
                pattern.Push(new SpawnCommand(12, 3, 3, a, true));
                pattern.Push(new SpawnCommand(12, 0, 0, b, true));
                pattern.Push(new SpawnCommand(12, -3, 0, c, true));
                break;
            case 10:
                pattern.Push(new SpawnCommand(-12, -12, 3f, a, true));
                pattern.Push(new SpawnCommand(-12, 12, 0.2f, b, true));
                pattern.Push(new SpawnCommand(-12, -12, 0.3f, c, true));
                pattern.Push(new SpawnCommand(-12, 12, 0.2f, a, true));
                break;
        }
    }

    // Update is called once per frame
    void Update() {

    }

    //tells you if the pattern is complete
    public bool isComplete() {   
        if (pattern.Count == 0)
            return true;
        return false;
    }

	public bool isWaiting() {
		return wait;
	}

    //commented until enemy code is done
    public SpawnCommand getNext() {
        return pattern.Pop();
    }   
}
