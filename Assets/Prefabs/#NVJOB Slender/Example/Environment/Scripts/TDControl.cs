using System;
using UnityEngine;

[AddComponentMenu("#NVJOB/Tools/TDControl")]
public class TDControl : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 180;
    public Vector2 mouseVerticaleClamp = new Vector2(-40, 40);
    public float smoothMouse = 5;

    [Header("Flight & Awareness")]
    public float baseFlySpeed = 8f;
    public float awareness = 0f;
    private float awarenessGainedPerSecond = 20f;

    [Header("Camera Settings")]
    public Transform camTransform;

    Transform tr;
    float mouseX, mouseY;
    float initialYRotation; // To store only the side-to-side start

    void Awake()
    {
        tr = transform;

        // FIX: Instead of taking the full tilted rotation, we only take the Y (side-to-side)
        // This stops the camera from 'starting' in the sky.
        initialYRotation = tr.eulerAngles.y;

        // Reset X and Z so we start looking level
        mouseX = 0;
        mouseY = 0;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        HandleAwarenessLogic();
        HandleRotation();
        HandleMovement();
    }

    void HandleAwarenessLogic()
    {
        float lookAngle = Vector3.Dot(camTransform.forward, Vector3.up);
        if (lookAngle > -0.2f) awareness += awarenessGainedPerSecond * Time.deltaTime;
        else awareness -= awarenessGainedPerSecond * 2f * Time.deltaTime;
        awareness = Mathf.Clamp(awareness, 0, 100);
    }

    void HandleRotation()
    {
        // Half-sensitivity for mouse movement
        mouseX += rotationSpeed * 0.01f * Input.GetAxis("Mouse X") * 0.15f;
        mouseY -= rotationSpeed * 0.01f * Input.GetAxis("Mouse Y") * 0.15f;

        mouseY = Mathf.Clamp(mouseY, mouseVerticaleClamp.x, mouseVerticaleClamp.y);

        // HALF-WEIGHT: Reduced from 0.5f to 0.25f so it doesn't bow your head so aggressively
        float bowEffect = awareness * 0.25f;

        // Apply rotation starting from a level horizon
        tr.rotation = Quaternion.Slerp(tr.rotation,
            Quaternion.Euler(mouseY + bowEffect, mouseX + initialYRotation, 0),
            smoothMouse * Time.deltaTime);
    }

    void HandleMovement()
    {
        float weightMultiplier = Mathf.Clamp(1f - (awareness / 100f), 0.05f, 1f);
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDir = (tr.forward * moveZ) + (tr.right * moveX);
        moveDir.y = 0;

        tr.position += moveDir.normalized * (baseFlySpeed * weightMultiplier) * Time.deltaTime;
    }
}