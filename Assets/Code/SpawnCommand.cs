using UnityEngine;
using System.Collections;

public class SpawnCommand {

    private float x;
    private float y;
    private float delay;
    private Color color;
    private bool isfixed;
    
    public SpawnCommand (float x, float y, float delay, Color color) : this (x, y, delay, color, false) {}

    public SpawnCommand(float x, float y, float delay, Color color, bool isfixed) {
        this.x = x;
        this.y = y;
        this.delay = delay;
        this.color = color;
        this.isfixed = isfixed;
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
    
    public bool isFixed() {
        return isfixed;
    }
}
