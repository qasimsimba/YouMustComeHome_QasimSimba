using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI textDisplay;

    [Header("Settings")]
    public float typeSpeed = 0.05f;

    [Header("Scene Transition Logic")]
    public int totalDialogueBlocks = 3; // Set this to the number of cubes you have
    private int completedDialogues = 0;
    public string nextSceneName = "Level2";

    private Queue<string> sentences = new Queue<string>();
    private bool isTyping = false;
    private string currentSentence;

    public static DialogueManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartDialogue(string[] lines)
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(true);

        // Freeze Player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            if (agent != null) agent.isStopped = true;
        }

        sentences.Clear();
        foreach (string line in lines) sentences.Enqueue(line);

        DisplayNextSentence();
    }

    void Update()
    {
        if (dialoguePanel != null && dialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                textDisplay.text = currentSentence;
                isTyping = false;
            }
            else
            {
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
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(false);

        // Unfreeze Player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            if (agent != null) agent.isStopped = false;
        }

        // INCREMENT THE COUNTER
        completedDialogues++;

        // CHECK IF ALL BLOCKS ARE DONE
        if (completedDialogues >= totalDialogueBlocks)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}