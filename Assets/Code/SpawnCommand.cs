using UnityEngine;
using System.Collections;

public class SpawnCommand {

    private float x;
    private float y;
    private float delay;
    private Color color;
    private string type;
    private bool left;
    
    public SpawnCommand(float x, float y, float delay, Color color) : this(x, y, delay, color, "normal", false) {}

    public SpawnCommand(float x, float y, float delay, Color color, string type) : this(x, y, delay, color, type, false) {}

    public SpawnCommand(float x, float y, float delay, Color color, string type, bool left) {
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

    new public string GetType () {
        return type;
    }

    public bool IsGoingLeft () {
        return left;
    }
}
