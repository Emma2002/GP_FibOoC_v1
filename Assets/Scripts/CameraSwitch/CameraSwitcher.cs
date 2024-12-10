using System.Collections;
using UnityEngine;
using DialogueEditor;

public class SmoothCameraSwitcher : MonoBehaviour
{
    public Camera Camera1; // Assign in the Inspector
    public Camera Camera2; // Assign in the Inspector
    private ConversationManager conversationManager; // Reference to the ConversationManager

    private bool isTransitioning = false; // Track if the camera is transitioning
    private bool conversationEnded = false; // Flag to track if the conversation has ended

    private void Start()
    {
        // Find the ConversationManager in the scene
        conversationManager = Object.FindFirstObjectByType<ConversationManager>();

        if (conversationManager == null)
        {
            Debug.LogError("ConversationManager not found in the scene!");
        }
        else
        {
            // Subscribe to the static OnConversationEnded event
            ConversationManager.OnConversationEnded += OnConversationEnded;
        }
    }

    // Callback for when the conversation ends
    private void OnConversationEnded()
    {
        conversationEnded = true;
    }

    private void Update()
    {
        if (conversationManager == null || !conversationEnded)
        {
            return; // Exit if the conversationManager is null or conversation has not ended
        }

        // Get the value of 'SwitchToSkinTissue' boolean from the ConversationManager
        bool switchToSkinTissue = conversationManager.GetBool("SwitchToSkinTissue");

        // If the switch flag is set and no transition is in progress, perform the camera switch
        if (switchToSkinTissue && !isTransitioning)
        {
            StartCoroutine(SmoothSwitch()); // Start the camera switch coroutine
            conversationEnded = false; // Reset the flag to allow the switch again after the next conversation
        }
    }

    private IEnumerator SmoothSwitch()
    {
        isTransitioning = true; // Mark the transition as in progress

        // Immediately disable the first camera and enable the second one
        Camera1.gameObject.SetActive(false); // Disable Camera1
        Camera2.gameObject.SetActive(true);  // Enable Camera2

        // Optionally enable the audio listener for the new camera
        AudioListener fromListener = Camera1.GetComponent<AudioListener>();
        AudioListener toListener = Camera2.GetComponent<AudioListener>();
        toListener.enabled = true; // Enable AudioListener on Camera2

        isTransitioning = false; // Transition is complete
        yield return null;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnConversationEnded event to prevent memory leaks
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }
}
