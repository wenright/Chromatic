using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Ring shaped Deck
 */ 
public class RingCardDeck : CardDeckInterface
{
    [Tooltip("Ring card deck radius")]
    public float radius = 5;

    // Use this for initialization
    protected override void Init()
    {
        loop = true;
    }

    protected override void ReArrangeCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            float currentAngle = ((float)(i - offsett) / Size()) * (Mathf.PI * 2);
            Vector3 pos = new Vector3(Mathf.Sin(currentAngle), 0, Mathf.Cos(currentAngle)) * -radius;
            if(cards[i] != null)
                cards[i].transform.localPosition = pos;
        }
    }

    protected override void IndexUpdate()
    {

        if (index < 0) index = 0;
        if (index > cards.Count - 1) index = cards.Count - 1;

        if (cards.Count <= 0) return;

        float target = ((float)index / cards.Count) * 360f;
        float offsetInDegree = (offsett / cards.Count) * 360.0f;

        if (offsetInDegree != target)
        {
            if (offsetInDegree < target)
            {
                offsetInDegree = Mathf.LerpAngle(offsetInDegree, target, 0.1f * cardMoveSpeed);

                if (target - offsetInDegree < 0.01f)
                    offsetInDegree = target;
            }

            if (offsetInDegree > target)
            {
                offsetInDegree = Mathf.LerpAngle(offsetInDegree, target, 0.1f * cardMoveSpeed);

                if (offsetInDegree - target < 0.01f)
                    offsetInDegree = target;
            }

            offsett = (offsetInDegree / 360.0f) * cards.Count;
        }
        
    }

}
