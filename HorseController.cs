using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HorseController : MonoBehaviour
{

    private GameObject gameState;
    private float baseSpeed = 0.0f;
    private bool racing;
    private GameState state;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("gameState");
        state = gameState.GetComponent<GameState>();

        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        racing = state.GetRacingState();

        Debug.Log(animator.GetBool("isRunning"));

        // Play running animation when racing.
        if (racing)
            animator.SetBool("isRunning", true);

        baseSpeed = racing ?  Random.Range(2, 6) : 0; 
        float randFactor = racing ? Random.value * Random.Range(2, 3) : 0;
        gameObject.transform.Translate(baseSpeed * randFactor * Time.deltaTime, 0, 0);
    }
}