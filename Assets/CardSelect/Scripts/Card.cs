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

    public Color color;

    private GooglePlayController googlePlayController;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer lockSpriteRenderer;

    private bool locked = false;

    void Awake () {
        GameObject googlePlayObject = GameObject.FindWithTag("GooglePlayController");
        if (googlePlayObject != null) {
            googlePlayController = googlePlayObject.GetComponent<GooglePlayController>();
        } else {
            Debug.LogError("Card: Unable to find GooglePlay object!");
        }

        spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.color = new Color(color.r, color.g, color.b, 0);
        spriteRenderer.DOFade(1.0f, 0.25f).SetEase(Ease.InQuad);

        lockSpriteRenderer = GetComponent<SpriteRenderer>();

        // TODO make sure these are the correct achievements
        if (tooltipMessage == "Skull")
        {
            if (!googlePlayController.IsAchievementUnlocked(Achievements.youDontGetThis)) {
                LockCard();
            }
        }
        else if (tooltipMessage == "Circle")
        {
            // This one is unlocked by default
        }
        else if (tooltipMessage == "Star")
        {
            if (!googlePlayController.IsAchievementUnlocked(Achievements.score5000)) {
                LockCard();
            }
        }
        else if (tooltipMessage == "Square")
        {
            if (!googlePlayController.IsAchievementUnlocked(Achievements.kill10)) {
                LockCard();
            }
        }
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
        spriteRenderer.DOColor(new Color(color.r / 2, color.g / 2, color.b / 2, 1.0f), 0.1f).SetEase(Ease.OutQuad);
    }

    void OnMouseUp()
    {
        if (this.transform.position.z == -1.6f)
        {
            // TODO would this be more readable as a switch statement?
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
                SceneManager.LoadScene("Skins");
            }
            else if (tooltipMessage == "Settings")
            {
                Debug.Log("Settings pressed");
            }
            // TODO maybe split the skins into a different skincards script
            else if (tooltipMessage == "Skull")
            {
                SelectSkin(SkinController.Skins.skull);
            }
            else if (tooltipMessage == "Circle")
            {
                SelectSkin(SkinController.Skins.circle);
            }
            else if (tooltipMessage == "Star")
            {
                SelectSkin(SkinController.Skins.star);
            }
            else if (tooltipMessage == "Square")
            {
                SelectSkin(SkinController.Skins.square);
            }
        }

        spriteRenderer.DOColor(color, 0.1f).SetEase(Ease.InQuad);
    }

    private void LockCard () {
        locked = true;

        if (lockSpriteRenderer) {
            lockSpriteRenderer.color = ColorList.blue;
        }
    }

    private void SelectSkin (SkinController.Skins skin) {
        if (!locked) {
            PlayerPrefs.SetInt("skin", (int) skin);
            SceneManager.LoadScene("Menu");
        }
    }
}
