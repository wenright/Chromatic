using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPattern {

    Stack<SpawnCommand> pattern = new Stack<SpawnCommand>();
	bool wait = false;
	Color a;
	Color b;
	Color c;

    // Calculate screen width and height in terms of world units
    private static float fixedHeight = Camera.main.orthographicSize;
    private static float fixedWidth = fixedHeight * Screen.width / Screen.height;
    private static float height = fixedHeight * 1.2f;
    private static float width = fixedWidth * 1.2f;

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
				pattern.Push (new SpawnCommand (width, height, 3, a));
				wait = true;
				break;
			case 2:
				pattern.Push (new SpawnCommand (width, height, 0, b));
				pattern.Push (new SpawnCommand (-width, height, 0, c));
				wait = true;
				break;
			case 3:
				pattern.Push (new SpawnCommand (width, height, 3, b));
				pattern.Push (new SpawnCommand (width, 0, 0, b));
				pattern.Push (new SpawnCommand (width, -height, 0, b));
                wait = true;
                break;
            case 4:
				pattern.Push (new SpawnCommand (-width, height, 3, c));
				pattern.Push (new SpawnCommand (-width, 0, 0, c));
				pattern.Push (new SpawnCommand (-width, -height, 0, c));
                wait = true;
                break;
            case 5:
				pattern.Push (new SpawnCommand (width, -height, 3, a));
				pattern.Push (new SpawnCommand (0, height, 0, a));
				pattern.Push (new SpawnCommand (-width, -height, 0, a));
				wait = true;
				break;
			case 6:
                //FIXED
				float leveltimer = 0.2f;
				fixedHeight++;
				pattern.Push (new SpawnCommand (-fixedWidth, fixedHeight, leveltimer + 2f, c, "fixed"));
				pattern.Push (new SpawnCommand (-fixedWidth * (4 / 5f), fixedHeight, leveltimer, c, "fixed"));
				pattern.Push (new SpawnCommand (-fixedWidth * (3 / 5f), fixedHeight, leveltimer, c, "fixed"));
				pattern.Push (new SpawnCommand (-fixedWidth * (2 / 5f), fixedHeight, leveltimer, c, "fixed"));
				pattern.Push (new SpawnCommand (-fixedWidth * (1 / 5f), fixedHeight, leveltimer, c, "fixed"));
				pattern.Push (new SpawnCommand (0, fixedHeight, leveltimer, c, "fixed"));
				pattern.Push (new SpawnCommand (fixedWidth * (1 / 5f), fixedHeight, leveltimer, b, "fixed"));
				pattern.Push (new SpawnCommand (fixedWidth * (2 / 5f), fixedHeight, leveltimer, c, "fixed"));
				pattern.Push (new SpawnCommand (fixedWidth * (3 / 5f), fixedHeight, leveltimer, b, "fixed"));
				pattern.Push (new SpawnCommand (fixedWidth * (4 / 5f), fixedHeight, leveltimer, c, "fixed"));
				pattern.Push (new SpawnCommand (fixedWidth, fixedHeight, leveltimer, b, "fixed"));
				wait = false;
				fixedHeight--;
                break;
			case 7:
				fixedWidth++;
				pattern.Push (new SpawnCommand (-fixedWidth, fixedHeight / 2f, 3, a, "fixed"));
				pattern.Push (new SpawnCommand (-fixedWidth, 0, 0, b, "fixed"));
				pattern.Push (new SpawnCommand (-fixedWidth, -fixedHeight / 2f, 0, c, "fixed"));
				fixedWidth--;
	            break;
            case 8:
                leveltimer = 0.2f;
				fixedHeight++;
                //FIXED
                pattern.Push(new SpawnCommand(-fixedWidth, fixedHeight, leveltimer+2f, b, "fixed"));
                pattern.Push(new SpawnCommand(-fixedWidth * (4/5f), -fixedHeight, leveltimer, b, "fixed"));
                pattern.Push(new SpawnCommand(-fixedWidth * (3/5f), fixedHeight, leveltimer, b, "fixed"));
                pattern.Push(new SpawnCommand(-fixedWidth * (2/5f), -fixedHeight, leveltimer, a, "fixed"));
                pattern.Push(new SpawnCommand(-fixedWidth * (1/5f), fixedHeight, leveltimer, a, "fixed"));
                pattern.Push(new SpawnCommand(0, -fixedHeight, leveltimer, a, "fixed"));
                pattern.Push(new SpawnCommand(fixedWidth * (1/5f), fixedHeight, leveltimer, c, "fixed"));
                pattern.Push(new SpawnCommand(fixedWidth * (2/5f), -fixedHeight, leveltimer, c, "fixed"));
                pattern.Push(new SpawnCommand(fixedWidth * (3/5f), fixedHeight, leveltimer, c, "fixed"));
                pattern.Push(new SpawnCommand(fixedWidth * (4/5f), -fixedHeight, leveltimer, b, "fixed"));
                pattern.Push(new SpawnCommand(fixedWidth, fixedHeight, leveltimer, b, "fixed"));
                wait = false;
				fixedHeight--;
                break;
            case 9:
				fixedWidth++;
                pattern.Push(new SpawnCommand(fixedWidth, fixedHeight / 2f, 3, a, "fixed"));
                pattern.Push(new SpawnCommand(fixedWidth, 0, 0, b, "fixed"));
                pattern.Push(new SpawnCommand(fixedWidth, -fixedHeight / 2f, 0, c, "fixed"));
				fixedWidth--;
                break;
			case 10:
				fixedHeight++;
				fixedWidth++;
				pattern.Push(new SpawnCommand(-fixedWidth, -fixedWidth, 3f, a, "fixed"));
				pattern.Push(new SpawnCommand(-fixedWidth, fixedWidth, 0.3f, b, "fixed"));
				pattern.Push(new SpawnCommand(-fixedWidth, -fixedWidth, 0.3f, a, "fixed"));
				pattern.Push(new SpawnCommand(-fixedWidth, fixedWidth, 0.3f, b, "fixed"));
				pattern.Push(new SpawnCommand(-fixedWidth, -fixedWidth, 0.3f, a, "fixed"));
				pattern.Push(new SpawnCommand(-fixedWidth, fixedWidth, 0.3f, b, "fixed"));
				pattern.Push(new SpawnCommand(-fixedWidth, -fixedWidth, 0.3f, c, "fixed"));
				pattern.Push(new SpawnCommand(-fixedWidth, fixedWidth, 0.3f, a, "fixed"));
				pattern.Push(new SpawnCommand(-fixedWidth, -fixedWidth, 0.3f, c, "fixed"));
				pattern.Push(new SpawnCommand(-fixedWidth, fixedWidth, 0.3f, a, "fixed"));
				pattern.Push(new SpawnCommand(-fixedWidth, -fixedWidth, 0.3f, c, "fixed"));
				pattern.Push(new SpawnCommand(-fixedWidth, fixedWidth, 0.3f, a, "fixed"));
				fixedHeight--;
				fixedWidth--;
                break;
			case 11:
				pattern.Push (new SpawnCommand (0, -height, 3f, c, "normal"));
				pattern.Push (new SpawnCommand (0, height, 0f, c, "normal"));
				pattern.Push (new SpawnCommand (width, 0, 0f, b, "normal"));
				pattern.Push (new SpawnCommand (-width, 0, 0f, b, "normal"));
				wait = true;
                break;
		case 12:
				fixedWidth++;
				pattern.Push (new SpawnCommand (-fixedWidth, fixedHeight*(1.2f) / 2f, 5f, a, "curve", false));
				pattern.Push (new SpawnCommand (-fixedWidth, 0, 0.2f, b, "curve", false));
				pattern.Push (new SpawnCommand (-fixedWidth, -fixedHeight*(1.2f) / 2f, 0.2f, c, "curve", false));
				fixedWidth--;
                break;
        case 13:
            fixedWidth++;
            pattern.Push(new SpawnCommand(-fixedWidth, (fixedHeight+0.5f)*(0.6f), 5f, a, "curve", false));
            pattern.Push(new SpawnCommand(-fixedWidth, 0.5f, 0.2f, b, "curve", false));
            pattern.Push(new SpawnCommand(-fixedWidth, -(fixedHeight-0.5f) * (0.6f), 0.2f, c, "curve", false));
            pattern.Push(new SpawnCommand(fixedWidth+2, (fixedHeight + 0.5f) * (0.6f), 0f, a, "curve", true));
            pattern.Push(new SpawnCommand(fixedWidth+2, 0.5f, 0.2f, b, "curve", true));
            pattern.Push(new SpawnCommand(fixedWidth+2, -(fixedHeight - 0.5f) * (0.6f), 0.2f, c, "curve", true));
            fixedWidth--;
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
