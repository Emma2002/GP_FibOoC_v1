using UnityEngine;
using DialogueEditor;
using System.Collections.Generic; 

public class ConversationStartAtCellClick : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation; // Reference to the conversation asset
    [SerializeField] private Tag specificOrganTag; // Specific tag for the organ (e.g., fibroblast)
    [SerializeField] private string outlineTag = "OutlineSelect"; // General outline tag (kept as a string)

    private static int cellCount; // Counter for the number of clicks
    private static HashSet<GameObject> clickedCells = new HashSet<GameObject>(); // To store clicked fibroblast cells
    private static HashSet<Tag> triggeredConversations = new HashSet<Tag>(); // To store triggered conversations for cell types

    private bool canClick = true;
    private float lastClickTime = 0f; // Time of the last valid click
    private float clickDelay = 0.5f; // Delay in seconds to debounce clicks

    void Update()
    {

        // Debounce clicks based on time
        if (!canClick || Time.time - lastClickTime < clickDelay)
            return;

        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            lastClickTime = Time.time; // Update the time of the last click
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray from the camera to the mouse position

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                // Debugging hit object
                Debug.Log("Hit object: " + clickedObject.name);

                // Check for a Tags component on the clicked object
                Tags objectTags = clickedObject.GetComponent<Tags>();

                // Ignore clicks if the conversation is active
                if (ConversationManager.Instance.IsConversationStillActive)
                {
                    Debug.Log("Conversation is active. Ignoring click.");
                    return; // Exit early
                }

                // Handle fibroblast or other cell-specific behavior
                if (objectTags != null && objectTags.HasTag(specificOrganTag))
                {
                    // Prevent multiple clicks on the same cell object
                    if (clickedCells.Contains(clickedObject))
                    {
                        Debug.Log("This cell has already been clicked.");
                        return; // Exit early if the cell has already been clicked
                    }

                    // Prevent starting a new conversation for the same cell type
                    if (triggeredConversations.Contains(specificOrganTag))
                    {
                        Debug.Log($"A conversation for the cell type '{specificOrganTag}' has already been triggered.");
                        return; // Exit early if a conversation for this cell type has been triggered
                    }

                    // Mark this specific cell and its type as clicked/triggered
                    clickedCells.Add(clickedObject);
                    triggeredConversations.Add(specificOrganTag);
                    cellCount++;
                    Debug.Log("Cell clicked: " + clickedObject.name + ", Count: " + cellCount);

                    // Start the conversation
                    ConversationManager.Instance.StartConversation(myConversation);
                }
                // Handle other general outline tag behavior
                else if (clickedObject.CompareTag(outlineTag))
                {
                    Debug.Log("Outline selected but not the organ!");
                }
            }
            // Start the cooldown timer for clicking
            StartCoroutine(ResetClickFlag());
        }
    }

    private IEnumerator<WaitForSeconds> ResetClickFlag()
    {
        canClick = false; // Prevent further clicks during the cooldown
        yield return new WaitForSeconds(clickDelay); // Set the delay time here
        canClick = true; // Allow the next click
    }

    public static int returnCellCount()
    {
        return cellCount;
    }
}