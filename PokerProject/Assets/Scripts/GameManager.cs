using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public CardData[] cardsData;
    public Image[] playerHandSlots;
    public Button discardButton;
    public Button foldButton;
    public Button doubleDownButton;
    public HPDamageSystem hpDamageSystem;
    public Text playerHPTxt;
    public Text opponentHPTxt;

    private List<CardData> deck;
    private List<CardData> playerHand; 
    private bool hasFolded;
    private bool hasDoubledDown; 

    void Start()
    {
        InitializeDeck();
        ShuffleDeck();
        DealCards();
        discardButton.onClick.AddListener(DiscardSelectedCards);
        foldButton.onClick.AddListener(Fold);
        doubleDownButton.onClick.AddListener(DoubleDown);
        hpDamageSystem.UpdateHPText();
    }

    void InitializeDeck()
    {
        deck = new List<CardData>();
        playerHand = new List<CardData>();
        hasFolded = false;
        hasDoubledDown = false;


        for (int i = 0; i < cardsData.Length; i++)
        {
            deck.Add(cardsData[i]);
        }
    }

    void ShuffleDeck()
    {

        for (int i = 0; i < deck.Count; i++)
        {
            CardData temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    void DealCards()
    {

        for (int i = 0; i < 5; i++)
        {
            CardData card = deck[i];
            playerHand.Add(card);
            playerHandSlots[i].sprite = card.image;
        }


        HandRank playerHandRank = HandEvaluator.EvaluateHand(playerHand);
        int damageDealt = CalculateDamage(playerHandRank);
        hpDamageSystem.DealDamageToOpponent(damageDealt);

        if (playerHandRank == HandRank.RoyalFlush)
        {
            Debug.Log("Player SUMMONS EXODIA- I mean they have a royal flush!");
            hpDamageSystem.DealDamageToOpponent(50);
        }
    }

    void DiscardSelectedCards()
    {
        List<int> cardsToDiscard = new List<int>();

        for (int i = 0; i < playerHandSlots.Length; i++)
        {
            if (playerHandSlots[i].GetComponent<CardSlot>().IsSelected)
            {
                cardsToDiscard.Add(i);
            }
        }


        foreach (int index in cardsToDiscard)
        {
            CardData newCard = deck[playerHand.Count];
            playerHand[index] = newCard;
            playerHandSlots[index].GetComponent<CardSlot>().IsSelected = false;
            playerHandSlots[index].GetComponent<CardSlot>().highlightImage.enabled = false;
            playerHandSlots[index].sprite = newCard.image;
        }


        HandRank playerHandRank = HandEvaluator.EvaluateHand(playerHand);
        int damageDealt = CalculateDamage(playerHandRank);
        hpDamageSystem.DealDamageToOpponent(damageDealt);
    }

    void Fold()
    {
        hasFolded = true;
        hpDamageSystem.DealDamageToPlayer(5);
        Debug.Log("YOU HAVE FOLDED: PREPARE FOR FIVE DAMAGE");
        CheckGameOver();
    }

    void DoubleDown()
    {
        if (!hasDoubledDown)
        {
            hasDoubledDown = true;


            CardData newCard = deck[playerHand.Count];
            playerHand.Add(newCard);
            playerHandSlots[playerHand.Count - 1].sprite = newCard.image;


            HandRank playerHandRank = HandEvaluator.EvaluateHand(playerHand);
            int damageDealt = CalculateDamage(playerHandRank);
            hpDamageSystem.DealDamageToOpponent(damageDealt);
        }
        else
        {
            Debug.Log("You can only double down once per round.");
        }
    }

    int CalculateDamage(HandRank handRank)
    {
        return (int)handRank * 5;
    }

    void CheckGameOver()
    {
        if (hpDamageSystem.opponentHP <= 0)
        {
            Debug.Log("YOU WIN!");
        }
        else if (hpDamageSystem.playerHP <= 0)
        {
            Debug.Log("YOU LOSE!");
        }
    }
}
