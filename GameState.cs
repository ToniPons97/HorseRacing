using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject horseInstantiator;
    [SerializeField] private GameObject raceButton;
    private bool racing;

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
                horseInstantiator
                    .GetComponent<HorseInstantiator>()
                    .InitHorses();
        }
    }

    public bool GetRacingState()
    {
        return racing;
    }

    public void SetRacingState(bool newState)
    {
        racing = newState;
    }
}
