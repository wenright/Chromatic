using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
 * Simplpe Curved Card Deck, arranged using cosinus function
 */
public class SimpleCurvedCardDeck : CardDeckInterface
{
    [Tooltip("Cosinus Curve strength ")]
    public float curveStrength = 3.0f;

    /*
     * Arrange cards using Cosinus Curve
     */
    protected override void ReArrangeCards()
    {
        float minX = -sizeX / 2.0f;
        float maxX = sizeX / 2.0f;

        for (int i = 0; i < cards.Count; i++)
        {
            float posX = (-i + offsett) * cardSpacing;
            float posZ = (posX - minX) / (maxX - minX);

            Vector3 pos = new Vector3( posX, 0, Mathf.Cos(posZ * Mathf.PI * 2) * curveStrength);

            if(cards[i] != null)
                cards[i].transform.localPosition = pos;
        }
    }

    protected override void Init() { }

}
