using System.Collections;
using UnityEngine;
using DialogueEditor;

public class SmoothCameraSwitcher : MonoBehaviour
{
    public Camera Camera1; // Assign in the Inspector
    public Camera Camera2; // Assign in the Inspector

    private ConversationManager conversationManager; // Reference to the ConversationManager

    public float transitionSpeed = 2f; // Speed of the transition
    private bool isTransitioning = false;
    private float transitionProgress = 0f; // Track the transition progress
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

        if (switchToSkinTissue && !isTransitioning)
        {
            StartCoroutine(SmoothSwitch()); // Start the smooth switch coroutine
            conversationEnded = false; // Reset the flag
        }
    }

    private IEnumerator SmoothSwitch()
    {
        isTransitioning = true;
        Camera fromCamera = Camera1;
        Camera toCamera = Camera2;
        AudioListener fromListener = fromCamera.GetComponent<AudioListener>();
        AudioListener toListener = toCamera.GetComponent<AudioListener>();

        // Gradually transition the camera's position and rotation
        while (transitionProgress < 1f)
        {
            transitionProgress += Time.deltaTime * transitionSpeed;

            toCamera.transform.position = Vector3.Lerp(fromCamera.transform.position, toCamera.transform.position, transitionProgress);
            toCamera.transform.rotation = Quaternion.Lerp(fromCamera.transform.rotation, toCamera.transform.rotation, transitionProgress);

            yield return null;
        }

        // Finalize the transition by disabling the previous camera
        Camera1.gameObject.SetActive(false);
        Camera2.gameObject.SetActive(true);

        toListener.enabled = true;

        transitionProgress = 0f;
        isTransitioning = false;
    }

    private void OnDestroy()
    {
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }
}