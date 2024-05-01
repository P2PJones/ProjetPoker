using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDamageSystem : MonoBehaviour
{
    public Text playerHPTxt;
    public Text opponentHPTxt;

    public int playerHP = 50; 
    public int opponentHP = 50; 

    public void DealDamageToOpponent(int damage)
    {
        opponentHP -= damage;
        UpdateHPText();
    }

    public void DealDamageToPlayer(int damage)
    {
        playerHP -= damage;
        UpdateHPText();
    }

    public void UpdateHPText()
    {
        playerHPTxt.text = "Player HP: " + playerHP.ToString();
        opponentHPTxt.text = "Opponent HP: " + opponentHP.ToString();
    }

    public void ResetHP()
    {
        playerHP = 50;
        opponentHP = 50;
        UpdateHPText();
    }
}
