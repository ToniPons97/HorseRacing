using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject horse;
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
        for (int i = 0; i < 5; i++)
        {
            spawnY = 4 - i * 2;
            position = new(spawnX, spawnY, 0);
            GameObject newHorse = Instantiate(horse, position, Quaternion.identity);
            newHorse.name = "Horse " + (i + 1);
        }
    }
}
