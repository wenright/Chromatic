﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardText : MonoBehaviour {
    public CardDeckInterface cardDeck;

    public Text selectedCardText;

	// Update is called once per frame
    void Update () {
        Card currentCard = cardDeck.GetCurrentCard();

        if (currentCard != null)
            selectedCardText.text = currentCard.tooltipMessage;
    }
    
}
