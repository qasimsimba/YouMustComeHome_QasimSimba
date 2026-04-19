using System;
using UnityEngine;

public class TreeSway : MonoBehaviour
{
    // Variables must be at the top, outside of Start or Update
    public float swaySpeed = 1.5f;
    public float swayAmount = 5.0f;

    private float randomOffset;
    private Quaternion startRotation;

    void Start()
    {
        // This stores the tree's original position so it doesn't drift
        startRotation = transform.localRotation;

        // Using UnityEngine.Random to avoid the ambiguity error you had earlier
        // This makes each tree move at a different time so they aren't in sync
        randomOffset = UnityEngine.Random.Range(0f, 100f);
    }

    void Update()
    {
        // Calculate movement using a Sine wave
        float swish = Mathf.Sin(Time.time * swaySpeed + randomOffset) * swayAmount;

        // Apply the rotation locally
        transform.localRotation = startRotation * Quaternion.Euler(swish, 0, 0);
    }
}