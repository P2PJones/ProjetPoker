using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CardSuit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

public enum HandRank
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush,
    RoyalFlush
}

[System.Serializable]
public class CardData
{
    public int id;
    public string rank;
    public CardSuit suit;
    public Sprite image;
    public int rankValue;

    public CardData(int id, string rank, CardSuit suit, Sprite image, int rankValue)
    {
        this.id = id;
        this.rank = rank;
        this.suit = suit;
        this.image = image;
        this.rankValue = rankValue;
    }
}
public class HandEvaluator : MonoBehaviour
{
    public static HandRank EvaluateHand(List<CardData> hand)
    {

        hand.Sort((x, y) => x.rankValue.CompareTo(y.rankValue));

        bool isFlush = CheckFlush(hand);
        bool isStraight = CheckStraight(hand);

        if (isFlush && isStraight)
        {
            if (hand[0].rankValue == 10)
                return HandRank.RoyalFlush;
            else
                return HandRank.StraightFlush;
        }
        else if (CheckFourOfAKind(hand))
        {
            return HandRank.FourOfAKind;
        }
        else if (CheckFullHouse(hand))
        {
            return HandRank.FullHouse;
        }
        else if (isFlush)
        {
            return HandRank.Flush;
        }
        else if (isStraight)
        {
            return HandRank.Straight;
        }
        else if (CheckThreeOfAKind(hand))
        {
            return HandRank.ThreeOfAKind;
        }
        else if (CheckTwoPair(hand))
        {
            return HandRank.TwoPair;
        }
        else if (CheckOnePair(hand))
        {
            return HandRank.OnePair;
        }
        else
        {
            return HandRank.HighCard;
        }
    }

    private static bool CheckFlush(List<CardData> hand)
    {
        CardSuit firstSuit = hand[0].suit;
        return hand.All(card => card.suit == firstSuit);
    }

    private static bool CheckStraight(List<CardData> hand)
    {
        for (int i = 1; i < hand.Count; i++)
        {
            if (hand[i].rankValue != hand[i - 1].rankValue + 1)
            {
                return false;
            }
        }
        return true;
    }

    private static bool CheckFourOfAKind(List<CardData> hand)
    {
        for (int i = 0; i <= hand.Count - 4; i++)
        {
            if (hand[i].rankValue == hand[i + 3].rankValue)
            {
                return true;
            }
        }
        return false;
    }

    private static bool CheckFullHouse(List<CardData> hand)
    {
        return CheckThreeOfAKind(hand) && CheckOnePair(hand);
    }

    private static bool CheckThreeOfAKind(List<CardData> hand)
    {
        for (int i = 0; i <= hand.Count - 3; i++)
        {
            if (hand[i].rankValue == hand[i + 2].rankValue)
            {
                return true;
            }
        }
        return false;
    }

    private static bool CheckTwoPair(List<CardData> hand)
    {
        int pairCount = 0;
        for (int i = 0; i <= hand.Count - 2; i++)
        {
            if (hand[i].rankValue == hand[i + 1].rankValue)
            {
                pairCount++;
                i++;
            }
        }
        return pairCount >= 2;
    }

    private static bool CheckOnePair(List<CardData> hand)
    {
        for (int i = 0; i <= hand.Count - 2; i++)
        {
            if (hand[i].rankValue == hand[i + 1].rankValue)
            {
                return true;
            }
        }
        return false;
    }
}
