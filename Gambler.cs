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
    }

    // Update is called once per frame
    void Update()
    {
        CheckBets();
        UpdateBetMenuVisibility();
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
        string realHorseName = ConvertLabelToHorseName(horseName);
        if (betAmount <= money)
        {
            // Updating bets dictionary
            bets[realHorseName] += betAmount;

            // Updating bet display
            UpdateBetDisplay(horseName, bets[realHorseName]);

            // Updating gambler's money and money display.
            money -= betAmount;
            gameState.SetMoneyDisplay(money);
        }
    }

    public void ReduceBet(string horseName)
    {
        string realHorseName = ConvertLabelToHorseName(horseName);
        if (bets[realHorseName] - betAmount >= 0)
        {
            // Updating bets dictionary
            bets[realHorseName] -= betAmount;

            // Updating bet display
            UpdateBetDisplay(horseName, bets[realHorseName]);

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

    public void InitializeBets()
    {
        List<string> horsesNames = HorseNameManager.GetInstantiatedHorsesNames();

        for (int i = 0; i < horsesNames.Count; i++)
        {
            bets.Add(horsesNames[i], 0);
        }
    }



    private string ConvertLabelToHorseName(string label)
    {
        List<string> horsesNames = HorseNameManager.GetInstantiatedHorsesNames();

        string horseLabelIndex = label.Split(" ")[1];
        int.TryParse(horseLabelIndex, out int index);

        return horsesNames[index - 1];
    }

    private void UpdateBetMenuVisibility()
    {
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


    public void CalculateWin(string winningHorse)
    {
        Debug.Log(winningHorse);
        int winMultiplier = Random.Range(2, 5);

        if (bets[winningHorse] > 0)
        {
            Debug.Log("You won!");

            money += bets[winningHorse] * winMultiplier;
        }
    }

    public void ResetBets()
    {
        // This doesn't work, NullReferenceException
        //for (int i = 0; i < 5; i++)
        //{
        //    UpdateBetDisplay("Horse " + (i + 1), 0);
        //}

        bets.Clear();
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