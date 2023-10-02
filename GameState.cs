using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject horseInstantiator;
    [SerializeField] private GameObject raceButton;
    [SerializeField] private TMP_Text finalPositionsDisplay;

    [SerializeField] private TMP_Text moneyDisplay;
    [SerializeField] private GameObject gamblerGO;
    private Gambler gambler;

    private bool racing;

    private List<string> finishPositions;
    private GameObject finishLine;

    private void Start()
    {
        finishLine = GameObject.FindGameObjectWithTag("Finish");
        gambler = gamblerGO.GetComponent<Gambler>();

        moneyDisplay.text = "$" + gambler.GetMoney();
    }

    // Update is called once per frame
    void Update()
    {
        if (racing)
        {
            raceButton.SetActive(false);
        }
        else
        {
            raceButton.SetActive(true);

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

            finalPositionsDisplay.text = "";
            int posCount = 1;
            foreach (string horse in finishPositions)
            {
                finalPositionsDisplay.text += posCount + ". " + horse + "\n";
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
        if (racing && finalPositionsDisplay.text != "")
        {
            finalPositionsDisplay.text = "";
            finishPositions.Clear();
        }
    }
}
