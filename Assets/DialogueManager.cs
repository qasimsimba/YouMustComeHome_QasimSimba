using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI; // Required for NavMesh


public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI textDisplay;

    [Header("Settings")]
    public float typeSpeed = 0.05f;

    private Queue<string> sentences = new Queue<string>();
    private bool isTyping = false;
    private string currentSentence;

    public static DialogueManager Instance;

    void Awake()
    {
        // Setup Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartDialogue(string[] lines)
    {
        // Safety: Ensure the panel exists before trying to show it
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }
        else
        {
            Debug.LogError("DialoguePanel is missing from the Inspector slot on DialogueManager!");
            return;
        }

        // Freeze Player Movement
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
            }
        }

        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
    }

    void Update()
    {
        // Only listen for clicks if the dialogue is actually active
        if (dialoguePanel != null && dialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // If user clicks while typing, finish the sentence instantly
                StopAllCoroutines();
                textDisplay.text = currentSentence;
                isTyping = false;
            }
            else
            {
                // If sentence is finished, go to the next one
                DisplayNextSentence();
            }
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        textDisplay.text = "";
        isTyping = true;

        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            // Use WaitForSecondsRealtime so it works even if Time.timeScale is 0
            yield return new WaitForSecondsRealtime(typeSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        // SHIELD: Check if the panel exists before touching it
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("The DialoguePanel slot is EMPTY on the DialogueManager object!");
        }

        // SHIELD: Check if the player exists before unfreezing
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            if (agent != null) agent.isStopped = false;
        }
    }
}