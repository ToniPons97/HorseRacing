using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gambler : MonoBehaviour
{
    [SerializeField] int money = 100;
    [SerializeField] int betAmount = 5;
    private int currentBet = 0;
    private bool hasPlacedBet;
    private GameObject[] betMenus;
    private Dictionary<string, int> bets;
    private GameState gameState;
    
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("gameState")
                                .GetComponent<GameState>();

        bets = new Dictionary<string, int>();
        InitializeBets();


        //PrintBets();




        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetMoney()
    {
        return money;
    }

    public bool GetHasPlacedBet()
    {
        return hasPlacedBet;
    }

    public void SetHasPlacedBet(bool placed)
    {
        hasPlacedBet = placed;
    }

    public void AddBet(string horseName)
    {
        if (betAmount <= money)
        {
            // Updating bets dictionary
            bets[horseName] += betAmount;

            // Updating bet display
            UpdateBetDisplay(horseName, bets[horseName]);

            // Updating gambler's money and money display.
            money -= betAmount;
            gameState.SetMoney(money);
        }
    }

    public void ReduceBet(string horseName)
    {
        if (betAmount >= money)
        {
            // Updating bets dictionary
            bets[horseName] -= betAmount;

            // Updating bet display
            UpdateBetDisplay(horseName, bets[horseName]);

            // Updating gambler's money and money display.
            money += betAmount;
            gameState.SetMoney(money);
        }
    }

    private void UpdateBetDisplay(string horseName, int amount)
    {
        GameObject betDisplay = GameObject.FindGameObjectWithTag(horseName + " Bet");
        TMP_Text betDisplayText = betDisplay.GetComponent<TMP_Text>();
        betDisplayText.text = "$" + amount;

        
    }

    private void InitializeBets()
    {
        for (int i = 0; i < 5; i++)
        {
            bets.Add("Horse" + " " + (i + 1), 0);
        }
    }

    private void PrintBets()
    {
        foreach (var pair in bets)
        {
            Debug.Log(pair.Key + ": " + pair.Value);
        }
    }
}
