using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;

/*
 * Card class, can be compared for sorting
 * and have a tooltip message...
 */
public class Card : MonoBehaviour,IComparable {
    
    public string tooltipMessage = "this is a tooltip message";

    [HideInInspector]
    public int id = 0;

    private GooglePlayController googlePlayController;
    private SpriteRenderer spriteRenderer;
 
    void Awake () {
        GameObject googlePlayObject = GameObject.FindWithTag("GooglePlayController");
        if (googlePlayObject != null) {
            googlePlayController = googlePlayObject.GetComponent<GooglePlayController>();
        } else {
            Debug.LogError("Card: Unable to find GooglePlay object!");
        }

        spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.color = new Color(1, 1, 1, 0);
        spriteRenderer.DOFade(1.0f, 0.25f).SetEase(Ease.InQuad);
    }

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
        spriteRenderer.DOColor(Color.grey, 0.1f).SetEase(Ease.OutQuad);
    }

    void OnMouseUp()
    {
        if (this.transform.position.z == -1.6f)
        {
            if (tooltipMessage == "Play")
            {
                Debug.Log("Play pressed");
                SceneManager.LoadScene("Game");
            }
            else if (tooltipMessage == "Achievements")
            {
                Debug.Log("Achievements pressed");

                googlePlayController.ShowAchievementsUI();
            }
            else if (tooltipMessage == "Leaderboards")
            {
                Debug.Log("Leaderboards pressed");

                googlePlayController.ShowLeaderboardUI();
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

        spriteRenderer.DOColor(Color.white, 0.1f).SetEase(Ease.InQuad);
    }
}
