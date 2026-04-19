using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Adding [TextArea] makes the box bigger in the Inspector
    // Changing 'string' to 'string[]' creates the List with the + button
    [TextArea(3, 10)]
    public string[] dialogueLines;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is tagged "Player"
        if (other.CompareTag("Player") && !hasTriggered)
        {
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartDialogue(dialogueLines);
                hasTriggered = true;
            }
        }
    }
}