using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject horseInstantiator;
    [SerializeField] private GameObject raceButton;
    private TMP_Text finalPositionsText;
    private GameObject resultsDisplay;
    private TMP_Text moneyText;
    [SerializeField] private GameObject gamblerGO;
    private Gambler gambler;

    private bool racing;
    private bool hasPlacedBet;

    private List<string> finishPositions;
    private GameObject finishLine;

    private void Start()
    {
        finishLine = GameObject.FindGameObjectWithTag("Finish");
        gambler = gamblerGO.GetComponent<Gambler>();
        resultsDisplay = GameObject.FindGameObjectWithTag("Display");
        finalPositionsText = resultsDisplay.transform.GetChild(0).GetComponent<TMP_Text>();
        finalPositionsText.fontSize = 42.4f;

        moneyText = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<TMP_Text>();
        moneyText.text = "$" + gambler.GetMoney();

        hasPlacedBet = gambler.GetHasPlacedBet();
    }

    // Update is called once per frame
    void Update()
    {
        if (racing || !hasPlacedBet)
        {
            raceButton.SetActive(false);
            resultsDisplay.SetActive(false);
        }
        else
        {
            raceButton.SetActive(true);

            if (finalPositionsText.text.Length > 0)
            {
                resultsDisplay.SetActive(true);
            }
            else
            {
                resultsDisplay.SetActive(false);
            }

            GameObject[] horses = GameObject.FindGameObjectsWithTag("Horse");
            if (horses.Length != 5)
            {
                horseInstantiator
                    .GetComponent<HorseInstantiator>()
                    .InitHorses();
                return;
            }

            finishPositions = finishLine.GetComponent<HandleTrigger>()
                .GetFinishOrder();

            finalPositionsText.text = "";
            int posCount = 1;
            foreach (string horse in finishPositions)
            {
                finalPositionsText.text += posCount + ". " + horse + "\n";
                posCount++;

                if (posCount > 5)
                    posCount = 1;
            }

            return;
        }
    }

    public bool GetRacingState()
    {
        return racing;
    }

    public void SetRacingState(bool newState)
    {
        racing = newState;
        if (racing && finalPositionsText.text != "")
        {
            finalPositionsText.text = "";
            finishPositions.Clear();
        }
    }

    public void SetMoney(int amount)
    {
        moneyText.text = "$" + amount;
    }
    
    
}
