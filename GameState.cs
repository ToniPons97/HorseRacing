using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject horseInstantiator;
    [SerializeField] private GameObject raceButton;
    private TMP_Text finalPositionsText;
    private GameObject display;
    private TMP_Text moneyText;
    [SerializeField] private GameObject gamblerGO;
    private Gambler gambler;

    private bool racing;

    private List<string> finishPositions;
    private GameObject finishLine;

    private void Start()
    {
        finishLine = GameObject.FindGameObjectWithTag("Finish");
        gambler = gamblerGO.GetComponent<Gambler>();
        display = GameObject.FindGameObjectWithTag("Display");
        finalPositionsText = display.transform.GetChild(0).GetComponent<TMP_Text>();
        moneyText = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<TMP_Text>();

        moneyText.text = "$" + gambler.GetMoney();
    }

    // Update is called once per frame
    void Update()
    {
        if (racing)
        {
            raceButton.SetActive(false);
            display.SetActive(false);
        }
        else
        {
            raceButton.SetActive(true);

            if (finalPositionsText.text.Length > 0)
            {
                display.SetActive(true);
            }
            else
            {
                display.SetActive(false);
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
}
