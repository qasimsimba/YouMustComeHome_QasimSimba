using System;
using UnityEngine;

public class DarknessManager : MonoBehaviour
{
    [Header("UI Reference")]
    public CanvasGroup darknessCanvas; // Drag your DarknessOverlay image here

    [Header("Settings")]
    public float fadeSpeed = 0.05f;    // 0.05 = 20 seconds to full black
    public int totalSpotsToHit = 3;

    private float currentAlpha = 0f;
    private int spotsHit = 0;
    private bool isWon = false;

    void Start()
    {
        if (darknessCanvas != null) darknessCanvas.alpha = 0f;
    }

    void Update()
    {
        if (isWon) return;

        // Increase alpha over time
        currentAlpha += fadeSpeed * Time.deltaTime;
        currentAlpha = Mathf.Clamp01(currentAlpha);

        if (darknessCanvas != null)
            darknessCanvas.alpha = currentAlpha;

        if (currentAlpha >= 1f)
        {
            Debug.Log("GAME OVER: The darkness took you.");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Optional restart
        }
    }

    // This is the function the projectile calls
    public void ResetDarkness()
    {
        currentAlpha = 0f;
        if (darknessCanvas != null) darknessCanvas.alpha = 0f;

        spotsHit++;
        Debug.Log($"Spots Hit: {spotsHit}/{totalSpotsToHit}");

        if (spotsHit >= totalSpotsToHit)
        {
            isWon = true;
            Debug.Log("Victory! You lit all the spots.");
        }
    }
}