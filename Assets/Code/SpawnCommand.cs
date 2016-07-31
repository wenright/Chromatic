using UnityEngine;
using System.Collections;

public class SpawnCommand {

    private int x;
    private int y;
    private float delay;
    private Color color;
    
    public SpawnCommand (int x, int y, float delay, Color color) {
        this.x = x;
        this.y = y;
        this.delay = delay;
        this.color = color;
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
}
