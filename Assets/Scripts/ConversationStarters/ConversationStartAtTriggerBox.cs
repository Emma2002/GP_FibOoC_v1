using UnityEngine;
using System.Collections;
using DialogueEditor;

public class ConversationStartAtTriggerBox : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    private bool hasStartedConversation = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger area and the conversation hasn't started yet
        if (other.CompareTag("Player") && !hasStartedConversation)
        {
            hasStartedConversation = true; // Set the flag to prevent starting again
            ConversationManager.Instance.StartConversation(myConversation);
            Debug.Log("Conversation should start");
        }
    }
}