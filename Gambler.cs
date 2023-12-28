using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gambler : MonoBehaviour
{
    [SerializeField] int money = 100;
    [SerializeField] int betAmount = 5;
    private bool hasPlacedBet;
    private Dictionary<string, int> bets;
    private GameState gameState;
    private bool racing;
    private GameObject betMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("gameState")
                                .GetComponent<GameState>();

        bets = new Dictionary<string, int>();
        InitializeBets();
        betMenu = GameObject.FindGameObjectWithTag("BetMenu");

        //PrintBets();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBets();

        racing = gameState.GetRacingState();
        if (racing)
        {
            betMenu.SetActive(false);
        }
        else
        {
            betMenu.SetActive(true);
        }

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
            gameState.SetMoneyDisplay(money);
        }
    }

    public void ReduceBet(string horseName)
    {
        if (bets[horseName] - betAmount >= 0)
        {
            // Updating bets dictionary
            bets[horseName] -= betAmount;

            // Updating bet display
            UpdateBetDisplay(horseName, bets[horseName]);

            // Updating gambler's money and money display.
            money += betAmount;
            gameState.SetMoneyDisplay(money);
        }
    }

    private void UpdateBetDisplay(string horseName, int amount)
    {
        // Update bet text for a specific horse.

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

    public void CalculateWin(string winningHorse)
    {
        Debug.Log(winningHorse);
        int winMultiplier = Random.Range(2, 5);

        if (bets[winningHorse] > 0)
        {
            Debug.Log("You won!");

            money += bets[winningHorse] * winMultiplier;
            gameState.SetMoneyDisplay(money);
        }

    }

    public void ResetBets()
    {
        List<string> keys = new(bets.Keys);

        foreach (var key in keys)
        {
            Debug.Log(key);
            bets[key] = 0;

            // This isn't working
            //UpdateBetDisplay(key, 0);
        }
    }

    // Check if there are bets placed.
    private void CheckBets()
    {
        int betsSum = 0;
        foreach (var pair in bets)
        {
            betsSum += pair.Value;
        }

        if (betsSum > 0)
        {
            hasPlacedBet = true;
        }
        else
        {
            hasPlacedBet = false;
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
