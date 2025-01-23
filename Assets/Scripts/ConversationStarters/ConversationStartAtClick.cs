using UnityEngine;
using DialogueEditor;
using System.Collections.Generic;

public class ConversationStartAtClick : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation; // Reference to the conversation asset
    [SerializeField] private Tag specificOrganTag; // Specific tag for the organ
    [SerializeField] private string outlineTag = "OutlineSelect"; // General outline tag (kept as a string)

    private static HashSet<GameObject> clickedObjects = new HashSet<GameObject>(); // To track clicked objects
     private static HashSet<Tag> triggeredTags = new HashSet<Tag>(); // To track tags of clicked objects

    void Update()
    {
        // Debugging: Check for null references early
        if (myConversation == null)
        {
            Debug.LogError($"{name}: No conversation assigned to myConversation!", this);
            return;
        }

        if (specificOrganTag == null)
        {
            Debug.LogError($"{name}: No specificOrganTag assigned!", this);
            return;
        }

        if (Camera.main == null)
        {
            Debug.LogError("No camera tagged as MainCamera found in the scene!");
            return;
        }

        if (ConversationManager.Instance == null)
        {
            Debug.LogError("ConversationManager is not initialized! Ensure Dialogue Editor is properly set up.");
            return;
        }

        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray from the camera to the mouse position

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                // Check if the object has already been clicked
                if (clickedObjects.Contains(clickedObject))
                {
                    Debug.Log($"{clickedObject.name} has already been clicked. Ignoring.");
                    return; // Exit early if the object has already been clicked
                }

                // Check for a Tags component on the clicked object
                Tags objectTags = clickedObject.GetComponent<Tags>();

                if (objectTags != null)
                {
                    // Check if the object has the specific organ tag
                    if (objectTags.HasTag(specificOrganTag))
                    {
                        // Check if the previous conversation is finished (not active)
                        if (!ConversationManager.Instance.IsConversationStillActive)
                        {
                            // Mark the object as clicked
                            clickedObjects.Add(clickedObject);
                            triggeredTags.Add(specificOrganTag);

                            // Start the conversation
                            ConversationManager.Instance.StartConversation(myConversation);
                        }
                    }
                }

                // Check if the object has the general outline tag (string-based check)
                else if (clickedObject.CompareTag(outlineTag))
                {
                    // Optionally handle other outline-related behavior
                    Debug.Log("Outline selected but not the organ!");
                }
            }
        }
    }
    public static bool IsObjectClicked(GameObject obj)
    {
        return clickedObjects.Contains(obj);
    }
    public static bool IsTagTriggered(Tag tag)
    {
        return triggeredTags.Contains(tag);
    }
}
