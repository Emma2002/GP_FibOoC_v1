using UnityEngine;
using DialogueEditor;

public class ConversationStartAtClick : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation; // Reference to the conversation asset
    [SerializeField] private string targetTag = "OutlineSelect"; // Tag for the specific organ or object

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray from the camera to the mouse position

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(targetTag)) // Check if the clicked object has the correct tag
                {
                    // Check if the previous conversation is finished (not active)
                    if (!ConversationManager.Instance.IsConversationStillActive)
                    {
                        // Start the conversation
                        ConversationManager.Instance.StartConversation(myConversation);
                    }
                }
            }
        }
    }
}
