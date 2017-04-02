using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * Card Tooltip, put this on gameobject with text component
 */ 
[RequireComponent(typeof(Text))]
public class CardTooltip : MonoBehaviour
{
    // we try to make this class a singleton
    public static CardTooltip instance;

    // tooltip message UI display
    private Text text;

    // Use this for initialization
    private void Start()
    {
        text = GetComponentInChildren<Text>();
        instance = this;
    }

    // Set this tooltip message
    public void SetMessage(string message)
    {
        text.text = message;
    }

}
