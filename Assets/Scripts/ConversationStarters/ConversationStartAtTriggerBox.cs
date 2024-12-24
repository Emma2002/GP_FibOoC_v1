using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using TMPro;

public class ConversationStartAtTriggerBox : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    private bool hasStartedConversation = false;
    [SerializeField] private TextMeshProUGUI counterText; // Assign the TextMeshProUGUI in the Inspector
    [SerializeField] private int totalRooms = 5; // Total number of rooms to discover
     private static int discoveredRooms = 0; // Tracks how many rooms have been discovered

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger area and the conversation hasn't started yet
        if (other.CompareTag("Player") && !hasStartedConversation)
        {
            hasStartedConversation = true; // Set the flag to prevent starting again
            ConversationManager.Instance.StartConversation(myConversation);
            Debug.Log("Conversation should start");


              // Track room discovery
            discoveredRooms++;

            // Update the counter text
            if (counterText != null)
            {
                counterText.text = $"Chambers discovered: {discoveredRooms}/{totalRooms}";
            }
            Debug.Log($"Room discovered. Total discovered: {discoveredRooms}/{totalRooms}");
        }
    
        }
    }


