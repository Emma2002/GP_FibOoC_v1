using UnityEngine;
using DialogueEditor;

public class FinalConversationStarterResearch : MonoBehaviour
{
    [SerializeField] private NPCConversation finalConversation; // Final conversation
    [SerializeField] private int objectsRequired = 3; // Number of objects required to start the conversation

    private bool finalConversationStarted = false;

    private void Update()
    {
        // Debugging: Log the current selected count from ConversationStartAtResearchClick
        Debug.Log($"Selected Object Count: {ConversationStartAtResearchClick.returnCellCount()}/{objectsRequired}");

        // Trigger the final conversation when the required count is met
        if (!finalConversationStarted && ConversationStartAtResearchClick.returnCellCount() == objectsRequired)
        {
            if (!ConversationManager.Instance.IsConversationStillActive)
            {
                ConversationManager.Instance.StartConversation(finalConversation);
                finalConversationStarted = true; // Prevent multiple starts
                Debug.Log("Final conversation started.");
            }
        }
    }
}
