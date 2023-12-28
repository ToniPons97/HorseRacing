using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject horse;

    [SerializeField] private float wtf = 4;
    [SerializeField] private float multiplier = 2;

    private readonly float spawnX = -9.37f;
    private float spawnY;


    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        InitHorses();
    }

    public void InitHorses()
    {
        for (int i = 4; i >= 0; i--)
        {
            spawnY = wtf - i * multiplier;
            position = new(spawnX, spawnY, 0);
            GameObject newHorse = Instantiate(horse, position, Quaternion.identity);
            newHorse.name = HorseNameManager.GetRandomHorseName();
        }
    }
}
