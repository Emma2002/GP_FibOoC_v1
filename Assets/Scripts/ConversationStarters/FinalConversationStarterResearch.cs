using UnityEngine;
using DialogueEditor;
using System.Collections.Generic;

public class FinalConversationStarterResearch : MonoBehaviour
{
    [SerializeField] private NPCConversation finalConversation; // Final conversation
    [SerializeField] private Tag ratTag; // Tag for the rat
    [SerializeField] private Tag petriDishTag; // Tag for the petri dish
    [SerializeField] private Tag organOnChipTag; // Tag for the organ-on-chip

    private bool ratSelected = false;
    private bool petriDishSelected = false;
    private bool organOnChipSelected = false;

    private bool finalConversationStarted = false;

    private HashSet<GameObject> clickedObjects = new HashSet<GameObject>(); // Store clicked objects to prevent re-clicks

    void Update()
    {
        // Debugging: Check final conversation conditions
        Debug.Log($"Selections - Rat: {ratSelected}, Petri Dish: {petriDishSelected}, Organ-on-Chip: {organOnChipSelected}");

        // Start final conversation when all objects are selected
        if (!finalConversationStarted && ratSelected && petriDishSelected && organOnChipSelected)
        {
            if (!ConversationManager.Instance.IsConversationStillActive)
            {
                ConversationManager.Instance.StartConversation(finalConversation);
                finalConversationStarted = true; // Prevent multiple starts
                Debug.Log("Final conversation started.");
            }
        }
    }

    public void OnObjectClicked(GameObject clickedObject)
    {
        if (clickedObjects.Contains(clickedObject))
        {
            Debug.LogWarning($"Object {clickedObject.name} has already been clicked. Ignoring.");
            return; // Exit if the object has already been clicked
        }

        Tags objectTags = clickedObject.GetComponent<Tags>();

        if (objectTags == null)
        {
            Debug.LogWarning("Clicked object does not have a Tags component.");
            return;
        }

        // Check which object was clicked based on tags
        if (objectTags.HasTag(ratTag))
        {
            ratSelected = true;
            Debug.Log("Rat selected.");
        }
        else if (objectTags.HasTag(petriDishTag))
        {
            petriDishSelected = true;
            Debug.Log("Petri dish selected.");
        }
        else if (objectTags.HasTag(organOnChipTag))
        {
            organOnChipSelected = true;
            Debug.Log("Organ-on-chip selected.");
        }

        // Add the clicked object to the HashSet to prevent future clicks
        clickedObjects.Add(clickedObject);
    }
}
