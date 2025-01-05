using UnityEngine;
using DialogueEditor;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalConversationCells : MonoBehaviour
{
    [SerializeField] private NPCConversation finalCellConversation; // Reference to the final conversation
    private bool conversationStarted = false;

    [SerializeField] private int cellCountRequired = 2;

    private void Setup()
    {
        ConversationManager.Instance.SetBool("allCellsDiscovered", false);
    }
    private void Update() {
        // Debug.Log("FinalConversation counts " + ConversationStartAtTriggerBox.ReturnDiscoveredRooms() + " Rooms");
        if(ConversationStartAtCellClick.returnCellCount() == cellCountRequired && !conversationStarted)
        {
            if(!ConversationManager.Instance.IsConversationStillActive)
            {
                ConversationManager.Instance.SetBool("allCellsDiscovered", true);
                ConversationManager.Instance.StartConversation(finalCellConversation);
                conversationStarted = true;
            }
        }
    }
}
