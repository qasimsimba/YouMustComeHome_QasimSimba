
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    NavMeshAgent agent;

    [Header("Horror Settings")]
    public float stepDistance = 2.0f; // How many meters to move per click

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        //ADDED
        if (DialogueManager.Instance.dialoguePanel.activeSelf) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 1. Calculate the direction from Player to the Click point
                Vector3 direction = (hit.point - transform.position).normalized;

                // 2. Calculate a point only "stepDistance" away from the player
                Vector3 smallStepDestination = transform.position + (direction * stepDistance);

                // 3. Tell the agent to move to that limited point instead of the full click
                agent.SetDestination(smallStepDestination);
            }
        }
    }
}