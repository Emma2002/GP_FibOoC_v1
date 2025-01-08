using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private List<Camera> cameras; // List of cameras to switch between (assign in the Inspector)
    [SerializeField] private int defaultActiveCameraIndex = 0; // The index of the default active camera

    private ConversationManager conversationManager; // Reference to the ConversationManager
    private bool isTransitioning = false; // Track if the camera is transitioning
    private bool conversationEnded = false; // Flag to track if the conversation has ended

    private void Start()
    {
        // Find the ConversationManager in the scene
        conversationManager = Object.FindFirstObjectByType<ConversationManager>();

        if (conversationManager == null)
        {
            // Debug.LogError("ConversationManager not found in the scene!");
            return;
        }

        // Subscribe to the static OnConversationEnded event
        ConversationManager.OnConversationEnded += OnConversationEnded;

        // Activate the default camera at the start
        ActivateCamera(defaultActiveCameraIndex);
    }

    private void OnConversationEnded()
    {
        conversationEnded = true;
    }

    private void Update()
    {
        if (conversationManager == null || !conversationEnded || isTransitioning)
        {
            return; // Exit if no valid conversationManager, conversation hasn't ended, or a transition is in progress
        }

        // Check conditions for camera switching based on bools
        if (conversationManager.GetBool("SwitchToSkinTissue"))
        {
            StartCoroutine(SmoothSwitch(1)); // Switch to Camera 2 (index 1)
            conversationEnded = false;
        }
        else if (conversationManager.GetBool("SwitchToResearchLevel"))
        {
            StartCoroutine(SmoothSwitch(2)); // Switch to Camera 3 (index 2)
            conversationEnded = false;
        }
    }

    private IEnumerator SmoothSwitch(int targetCameraIndex)
    {
        isTransitioning = true; // Mark the transition as in progress

        // Disable all cameras first
        foreach (Camera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }

        // Enable the target camera
        cameras[targetCameraIndex].gameObject.SetActive(true);

        // Handle AudioListeners: enable only for the active camera
        foreach (Camera cam in cameras)
        {
            AudioListener listener = cam.GetComponent<AudioListener>();
            if (listener != null)
                listener.enabled = cam.gameObject.activeSelf;
        }

        yield return null; // Optional: wait for one frame if needed
        isTransitioning = false; // Mark the transition as complete
    }

    private void ActivateCamera(int cameraIndex)
    {
        // Ensure all cameras are deactivated except the one at cameraIndex
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].gameObject.SetActive(i == cameraIndex);
            AudioListener listener = cameras[i].GetComponent<AudioListener>();
            if (listener != null)
                listener.enabled = i == cameraIndex;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnConversationEnded event to prevent memory leaks
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }

    public bool IsCameraSwitchComplete()
    {
        return !isTransitioning; // Return true if the camera switch is complete
    }
}
