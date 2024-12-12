using System.Collections;
using UnityEngine;
using DialogueEditor;

public class ConversationStarterTissueLevel : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private SmoothCameraSwitcher cameraSwitcher; // Reference to the CameraSwitcher
    private ConversationManager conversationManager;
    private bool conversationStarted = false;
    [SerializeField] private float delayTime = 2f; // Delay time before starting the conversation (in seconds)

    void Update()
    {
        // Find ConversationManager if not already found
        if (conversationManager == null)
        {
            conversationManager = Object.FindFirstObjectByType<ConversationManager>();
        }

        // Ensure we have a conversation manager and conversation hasn't started yet
        if (conversationManager != null && !conversationStarted)
        {
            bool switchToSkinTissue = conversationManager.GetBool("SwitchToSkinTissue");
            
            if (switchToSkinTissue)
            {
                // Start a coroutine to wait before starting the conversation
                StartCoroutine(StartConversationWithDelay());
            }
        }
    }

    private IEnumerator StartConversationWithDelay()
{   
    // Wait for the camera to finish switching (assuming cameraSwitcher has a method like IsCameraSwitchComplete())
    while (!cameraSwitcher.IsCameraSwitchComplete()) 
    {
        yield return null; // Wait until the camera has finished its transition
    }

    // Wait a little longer before starting the conversation, if needed
    yield return new WaitForSeconds(delayTime);

    // Start the conversation
    ConversationManager.Instance.StartConversation(myConversation);
    conversationStarted = true; // Prevent multiple starts

    Debug.Log("Conversation started at Tissue Level!");
}
}
