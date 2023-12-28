using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTrigger : MonoBehaviour
{
    private GameObject gameStateGO;
    private GameObject gamblerGO;
    private GameState gameState;
    private Gambler gambler;
    private readonly List<string> finishPositions = new();
    private int exitCount;

    private void Start()
    {
        gameStateGO = GameObject.FindGameObjectWithTag("gameState");
        gameState = gameStateGO.GetComponent<GameState>();

        gamblerGO = GameObject.FindGameObjectWithTag("Gambler");
        gambler = gamblerGO.GetComponent<Gambler>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        finishPositions.Add(collision.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        exitCount++;

        if (exitCount == 5)
        {

            // Set racing state to false.
            gameState.SetRacingState(false);

            // Set hasPlacedBets to false.            
            gambler.SetHasPlacedBet(false);

            // Calculate win
            gambler.CalculateWin(finishPositions[0]);




            // Destroy horses.
            GameObject[] horses = GameObject.FindGameObjectsWithTag("Horse");
            foreach (GameObject horse in horses)
            {
                Destroy(horse);
            }

            HorseNameManager.ResetUsedNamesList();


            // Reset bets
            gambler.ResetBets();

            // Reset exitCount.
            exitCount = 0;
        }
    }

    public List<string> GetFinishOrder()
    {
        return finishPositions;
    }
}
