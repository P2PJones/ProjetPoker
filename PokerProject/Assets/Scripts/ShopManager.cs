
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text soulsText;
    public Button raviosHoodButton;
    public Button morshusBombsButton;
    public Button psiHealButton;
    public Text tooltipText;
    public GameObject shopUI;

    private PlayerStats playerStats;
    private int playerSouls; 

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>(); 
        playerSouls = playerStats.GetSouls();
        UpdateSoulsText();
        CheckItemAvailability();


        raviosHoodButton.onClick.AddListener(BuyRaviosHood);
        raviosHoodButton.onClick.AddListener(() => ShowTooltip("Ravio's Hood: A mysterious hood once worn by Lorule's Best Merchant (Halved Damage) - (2 souls)"));
        raviosHoodButton.onClick.AddListener(HideTooltip);

        morshusBombsButton.onClick.AddListener(BuyMorshusBombs);
        morshusBombsButton.onClick.AddListener(() => ShowTooltip("Morshus' Bombs: You want it? It's yours my friend. (Double Damage) - (5 souls)"));
        morshusBombsButton.onClick.AddListener(HideTooltip);

        psiHealButton.onClick.AddListener(BuyPsiHeal);
        psiHealButton.onClick.AddListener(() => ShowTooltip("PSI Heal: Boing! Zoom! (Heals 5 HP per round) - (10 souls)"));
        psiHealButton.onClick.AddListener(HideTooltip);
    }

    void UpdateSoulsText()
    {
        soulsText.text = "Souls: " + playerSouls;
    }

    void CheckItemAvailability()
    {

        raviosHoodButton.interactable = playerSouls >= 2 && !playerStats.HasRaviosHood();
        morshusBombsButton.interactable = playerSouls >= 5 && !playerStats.HasMorshusBombs();
        psiHealButton.interactable = playerSouls >= 10 && !playerStats.HasPsiHeal();
    }

    void BuyRaviosHood()
    {
        if (playerSouls >= 2 && !playerStats.HasRaviosHood())
        {
            playerStats.AddSouls(-2);
            playerStats.EnableRaviosHood();
            UpdateSoulsText();
            CheckItemAvailability();
        }
    }

    void BuyMorshusBombs()
    {
        if (playerSouls >= 5 && !playerStats.HasMorshusBombs())
        {
            playerStats.AddSouls(-5);
            playerStats.EnableMorshusBombs();
            UpdateSoulsText();
            CheckItemAvailability();
        }
    }

    void BuyPsiHeal()
    {
        if (playerSouls >= 10 && !playerStats.HasPsiHeal())
        {
            playerStats.AddSouls(-10);
            playerStats.EnablePsiHeal();
            UpdateSoulsText();
            CheckItemAvailability();
        }
    }
    void ShowTooltip(string tooltip)
    {
        tooltipText.text = tooltip;
        tooltipText.gameObject.SetActive(true);
    }

    void HideTooltip()
    {
        tooltipText.gameObject.SetActive(false);
    }

    public void ToggleShopUI()
    {

        shopUI.SetActive(!shopUI.activeSelf);
    }

    public void CloseShopUI()
    {

        shopUI.SetActive(false);
    }
}

