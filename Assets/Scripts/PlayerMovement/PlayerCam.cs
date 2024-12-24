using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class PlayerCam : MonoBehaviour
{
    public float sensX = 100f;
    public float sensY = 100f;

    public Transform orientation;

    private float xRotation;
    private float yRotation;
    private bool isCursorLocked = true; // Keep track of cursor state manually

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        // Handle cursor locking and visibility based on conversation state
        if (ConversationManager.Instance.IsConversationStillActive)
        {
            if (isCursorLocked)
            {
                UnlockCursor(); // Ensure cursor is unlocked during conversations
            }
        }
        else
        {
            if (!isCursorLocked)
            {
                LockCursor(); // Re-lock cursor when conversation ends
            }

            // Process mouse input only when cursor is locked
            HandleMouseInput();
        }
    }

    private void HandleMouseInput()
    {
        // Read mouse input for rotation
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Update rotation values
        yRotation += mouseX;
        xRotation -= mouseY;

        // Clamp xRotation to prevent over-rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotations
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;

        // Re-sync camera rotation with current transform
        Vector3 currentRotation = transform.eulerAngles;
        xRotation = NormalizeAngle(currentRotation.x);
        yRotation = NormalizeAngle(currentRotation.y);
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
    }

    private float NormalizeAngle(float angle)
    {
        // Normalize angle to the range -180 to 180 degrees
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }
}