using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueLines;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            if (DialogueManager.Instance != null)
            {
                // Tell the manager to start this specific set of lines
                DialogueManager.Instance.StartDialogue(dialogueLines);
                hasTriggered = true;
            }
        }
    }
}