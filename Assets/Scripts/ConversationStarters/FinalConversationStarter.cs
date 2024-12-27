using UnityEngine;
using DialogueEditor;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation finalConversation; // Reference to the final conversation
    private bool conversationStarted = false;

    private void Update() {
        Debug.Log("FinalConversation counts " + ConversationStartAtTriggerBox.ReturnDiscoveredRooms() + " Rooms");
        if(ConversationStartAtTriggerBox.ReturnDiscoveredRooms() == 5 && !conversationStarted)
        {
            if(!ConversationManager.Instance.IsConversationStillActive)
            {
                ConversationManager.Instance.StartConversation(finalConversation);
                conversationStarted = true;
            }
        }
        if(ConversationManager.Instance.GetBool("SwitchEndMenu") == true)
        {
            SceneManager.LoadScene("04_EndMenu");
        }
    }
}
