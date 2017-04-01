﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
/*
 * Card class, can be compared for sorting
 * and have a tooltip message...
 */
public class Card : MonoBehaviour,IComparable {
    
    public string tooltipMessage = "this is a tooltip message";

    [HideInInspector]
    public int id = 0;
 
    // Compare based on card id
    int IComparable.CompareTo(object obj)
    {
        Card otherCard = obj as Card;
        if(otherCard != null)
        {
            return id.CompareTo(otherCard.id);
        }
        else
        {
            return 0;
        }
        
    }

    void OnMouseDown()
    {
        if (this.transform.position.z == -1.6f)
        {
            if (tooltipMessage == "Play")
            {
                Debug.Log("Play pressed");
                SceneManager.LoadScene("Will Test");
            }
            else if (tooltipMessage == "Achievements")
            {
                Debug.Log("Achievements pressed");
            }
            else if (tooltipMessage == "Leaderboards")
            {
                Debug.Log("Leaderboards pressed");
            }
            else if (tooltipMessage == "Skins")
            {
                Debug.Log("Skins pressed");
            }
            else if (tooltipMessage == "Settings")
            {
                Debug.Log("Settings pressed");
            }



        }
    }
}
