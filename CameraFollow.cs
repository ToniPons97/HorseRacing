using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private List<Transform> transforms = new();  // List of horse transforms to follow
    [SerializeField] private Vector3 offset;      // Offset from the horses' position
    private Camera mainCamera;

    private GameObject gameState;
    private GameState state;

    private Vector3 initialPosition;


    [SerializeField] private float minOrthoSize = 5.0f; // Minimum orthographic size
    [SerializeField] private float maxOrthoSize = 8.0f; // Maximum orthographic size
    [SerializeField] private float orthoSizeLerpSpeed = 2.0f; // Speed at which orthographic size changes

    private void Start()
    {
        mainCamera = Camera.main;
        gameState = GameObject.FindGameObjectWithTag("gameState");
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Don't follow horses until race has started.
        state = gameState.GetComponent<GameState>();

        if (!state.GetRacingState())
        {
            transform.position = new Vector3(0, 0, -10);
            return;
        }

        FindHorses();

        if (transforms.Count == 0)
            return;

        // Calculate the average position of all the horses
        Vector3 averagePosition = Vector3.zero;
        foreach (Transform horse in transforms)
            averagePosition += horse.position;

        averagePosition /= transforms.Count;

        // Apply the offset to the average position
        Vector3 targetPosition = averagePosition + offset;

        // Smoothly move the camera toward the target position
        Vector3 newCameraPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        newCameraPosition.z = -10.0f;
        transform.position = newCameraPosition;

        // Adjust the orthographic size based on the distance between horses
        AdjustOrthographicSize();
    }

    private void FindHorses()
    {
        transforms.Clear();
        GameObject[] horses = GameObject.FindGameObjectsWithTag("Horse");

        foreach (GameObject h in horses)
        {
            transforms.Add(h.transform);
        }
    }

    private void AdjustOrthographicSize()
    {
        float maxDistance = 0.0f;

        foreach (Transform horse in transforms)
        {
            float distance = Vector3.Distance(horse.position, transform.position);

            if (distance > maxDistance)
                maxDistance = distance;
        }

        float targetOrthoSize = Mathf.Clamp(maxDistance, minOrthoSize, maxOrthoSize);
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetOrthoSize, Time.deltaTime * orthoSizeLerpSpeed);
    }
}
