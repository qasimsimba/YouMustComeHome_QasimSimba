using UnityEngine;
using ithappy.Animals_FREE;

public class ReindeerBrain : MonoBehaviour
{
    private CreatureMover mover;
    private float timer;
    private float nextActionTime;

    private UnityEngine.Vector2 currentAxis;
    private UnityEngine.Vector3 currentTarget;
    private bool isRunning;

    void Start()
    {
        mover = GetComponent<CreatureMover>();
        PickNewAction();
        // Use UnityEngine specifically to avoid the System.Random conflict
        timer = UnityEngine.Random.Range(0f, nextActionTime);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextActionTime)
        {
            PickNewAction();
            timer = 0;
        }

        mover.SetInput(currentAxis, currentTarget, isRunning, false);
    }

    void PickNewAction()
    {
        // 50% chance to move, 50% chance to stay still
        if (UnityEngine.Random.value > 0.5f)
        {
            // Individual random directions for X and Y
            currentAxis = new UnityEngine.Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
            isRunning = false;
        }
        else
        {
            currentAxis = UnityEngine.Vector2.zero;
        }

        // Target for the deer to "look at" while moving
        currentTarget = transform.position + transform.forward * 10f;

        // Randomize how long they do this specific action
        nextActionTime = UnityEngine.Random.Range(2f, 6f);
    }
}