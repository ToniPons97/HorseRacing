using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    private GameObject gameState;
    private float baseSpeed;
    private bool racing;
    private GameState state;
    private Animator animator;

    [SerializeField] float minSpeed = 1.0f;
    [SerializeField] float maxSpeed = 4.0f;
    [SerializeField] float speedVariation = 1.5f; // Maximum variation to limit the difference in speed

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("gameState");
        state = gameState.GetComponent<GameState>();

        animator = gameObject.GetComponent<Animator>();

        // Assign a random speed within the specified range to each horse.
        baseSpeed = Random.Range(minSpeed, maxSpeed);


        SetHorseSpeed(baseSpeed);

        Debug.Log(gameObject.name + ": " + baseSpeed);
    }

    // Function to set the horse's speed
    private void SetHorseSpeed(float speed)
    {
        // Adjust the animation speed based on the horse's actual speed.
        animator.SetFloat("SpeedMultiplier", speed);
    }

    // Update is called once per frame
    void Update()
    {
        racing = state.GetRacingState();

        // Enable or disable renderer based on racing state.
        //GetComponent<SpriteRenderer>().enabled = racing;

        // Play running animation when racing.
        animator.SetBool("isRunning", racing);

        // Adjust speed slightly to limit the difference
        if (racing)
        {
            baseSpeed += Random.Range(-speedVariation, speedVariation);
            baseSpeed = Mathf.Clamp(baseSpeed, minSpeed, maxSpeed);

            float randFactor = Random.Range(2.0f, 3.0f);
            float moveSpeed = randFactor * baseSpeed;
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
    }
}
