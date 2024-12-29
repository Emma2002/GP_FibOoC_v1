using UnityEngine;
using DialogueEditor;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalConversationCells : MonoBehaviour
{
    [SerializeField] private NPCConversation finalCellConversation; // Reference to the final conversation
    private bool conversationStarted = false;

    [SerializeField] private int cellCountRequired = 2;

    private void Update() {
        Debug.Log("FinalConversation counts " + ConversationStartAtTriggerBox.ReturnDiscoveredRooms() + " Rooms");
        if(ConversationStartAtCellClick.returnCellCount() == cellCountRequired && !conversationStarted)
        {
            if(!ConversationManager.Instance.IsConversationStillActive)
            {
                ConversationManager.Instance.StartConversation(finalCellConversation);
                conversationStarted = true;
            }
        }
    }
}
