using UnityEngine;
using System.Collections;

public class SpawnCommand {

    private int x;
    private int y;
    private float delay;
    private Color color;
    private string type;
    private bool left;
    
    public SpawnCommand (int x, int y, float delay, Color color) {
        this.x = x;
        this.y = y;
        this.delay = delay;
        this.color = color;
        type = "normal";
    }
    public SpawnCommand(int x, int y, float delay, Color color, string type)
    {
        this.x = x;
        this.y = y;
        this.delay = delay;
        this.color = color;
        this.type = type;
    }

    public SpawnCommand(int x, int y, float delay, Color color, string type, bool left)
    {
        this.x = x;
        this.y = y;
        this.delay = delay;
        this.color = color;
        this.type = type;
        this.left = left;
    }

    public Vector2 GetLocation () {
        return new Vector2(x, y);
    }

    public float GetDelay () {
        return delay;
    }

    public Color GetColor () {
        return color;
    }
    public string GetType()
    {
        return type;
    }
    public bool IsGoingLeft()
    {
        return left;
    }
}
