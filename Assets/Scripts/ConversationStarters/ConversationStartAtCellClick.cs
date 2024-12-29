using UnityEngine;
using DialogueEditor;
using System.Collections.Generic;  // For HashSet

public class ConversationStartAtCellClick : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation; // Reference to the conversation asset
    [SerializeField] private Tag specificOrganTag; // Specific tag for the organ (e.g., fibroblast)
    [SerializeField] private string outlineTag = "OutlineSelect"; // General outline tag (kept as a string)
    private static int cellCount; // Counter for the number of clicks
    private static HashSet<GameObject> clickedCells = new HashSet<GameObject>(); // To store clicked fibroblast cells

    void Update()
    {    
        Debug.Log("CellCOUNTTTTT: " + cellCount);
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray from the camera to the mouse position

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                // Check for a Tags component on the clicked object
                Tags objectTags = clickedObject.GetComponent<Tags>();

                // Prevent multiple clicks on the same fibroblast cell
                if (objectTags != null && objectTags.HasTag(specificOrganTag))
                {
                    // If the fibroblast cell has already been clicked, exit early
                    if (clickedCells.Contains(clickedObject))
                    {
                        Debug.Log("This fibroblast cell has already been selected.");
                        return; // Exit early if the cell has already been selected
                    }

                    // Mark this fibroblast cell as clicked
                    clickedCells.Add(clickedObject);
                    cellCount++;

                    // Check if the previous conversation is finished (not active)
                    if (!ConversationManager.Instance.IsConversationStillActive)
                    {
                        // Start the conversation
                        ConversationManager.Instance.StartConversation(myConversation);
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

    public static int returnCellCount()
    {
        return cellCount;
    }
}
