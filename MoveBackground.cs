using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("gameState").GetComponent<GameState>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.GetRacingState())
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
}
