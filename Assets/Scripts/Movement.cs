using System;
using UnityEngine;

[AddComponentMenu("#NVJOB/Tools/TDControl")]
public class Movement : MonoBehaviour
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
    float initialYRotation;

    void Awake()
    {
        tr = transform;
        initialYRotation = tr.eulerAngles.y;

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
        mouseX += rotationSpeed * 0.01f * Input.GetAxis("Mouse X") * 0.15f;
        mouseY -= rotationSpeed * 0.01f * Input.GetAxis("Mouse Y") * 0.15f;

        mouseY = Mathf.Clamp(mouseY, mouseVerticaleClamp.x, mouseVerticaleClamp.y);

        float bowEffect = awareness * 0.25f;

        tr.rotation = Quaternion.Slerp(tr.rotation,
            Quaternion.Euler(mouseY + bowEffect, mouseX + initialYRotation, 0),
            smoothMouse * Time.deltaTime);
    }

    void HandleMovement()
    {
        float weightMultiplier = Mathf.Clamp(1f - (awareness / 100f), 0.05f, 1f);

        // Get Inputs
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // MANUAL INVERSION: If W went back, -moveZ makes it go forward.
        // If A went right, -moveX makes it go left.
        Vector3 moveDir = (tr.forward * -moveZ) + (tr.right * -moveX);

        // If it's STILL wrong after this, change the line above to:
        // Vector3 moveDir = (tr.forward * -moveZ) + (tr.right * -moveX);

        moveDir.y = 0;

        if (moveDir.magnitude > 0.1f)
        {
            tr.position += moveDir.normalized * (baseFlySpeed * weightMultiplier) * Time.deltaTime;
        }
    }
}