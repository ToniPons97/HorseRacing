using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTrigger : MonoBehaviour
{
    private GameObject gameState;
    private readonly List<string> finishPositions = new();
    private int exitCount;

    private void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("gameState");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        finishPositions.Add(collision.name);
        Debug.Log(collision.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        exitCount++;

        if (exitCount == 5)
        {
            // Destroy horses.
            GameObject[] horses = GameObject.FindGameObjectsWithTag("Horse");

            foreach (GameObject h in horses)
                Destroy(h);

            // Set racing state to false.
            GameState state = gameState.GetComponent<GameState>();
            state.SetRacingState(false);

            // Reset exitCount.
            exitCount = 0;
        }
    }
}
