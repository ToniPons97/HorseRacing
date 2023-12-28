using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject horseInstantiator;
    private Button raceButton;
    private GameObject raceButtonGO;
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
        raceButton = GameObject.FindGameObjectWithTag("RaceButton")
                                .GetComponent<Button>();

        raceButtonGO = raceButton.transform.parent.gameObject;

        finishLine = GameObject.FindGameObjectWithTag("Finish");
        gambler = gamblerGO.GetComponent<Gambler>();
        resultsDisplay = GameObject.FindGameObjectWithTag("Display");

        finalPositionsText = resultsDisplay.transform.GetChild(0)
                                .GetComponent<TMP_Text>();

        finalPositionsText.fontSize = 42.4f;

        moneyText = GameObject.FindGameObjectWithTag("MoneyText")
                                .GetComponent<TMP_Text>();

        moneyText.text = "$" + gambler.GetMoney();


    }

    // Update is called once per frame
    void Update()
    {   
        UpdateRaceButtonVisibility();
        UdateRaceResultsPanel();

        hasPlacedBet = gambler.GetHasPlacedBet();


        if (racing || !hasPlacedBet)
        {
            raceButton.interactable = false;
        }
        else
        {
            raceButton.interactable = true;

            GameObject[] horses = GameObject.FindGameObjectsWithTag("Horse");
            if (horses.Length != 5)
            {
                horseInstantiator
                    .GetComponent<HorseInstantiator>()
                    .InitHorses();
                return;
            }
        }
    }

    private void UpdateRaceButtonVisibility()
    {

        if (racing)
        {
            raceButtonGO.SetActive(false);
        }
        else
        {
            raceButtonGO.SetActive(true);
        }
    }

    private void UdateRaceResultsPanel()
    {
        if (!racing)
        {
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

    public void SetMoneyDisplay(int amount)
    {
        moneyText.text = "$" + amount;
    }
}