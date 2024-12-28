using UnityEngine;
using System.Collections;
using DialogueEditor;

public class HoverOutlineComplexObjects : MonoBehaviour
{
    private Renderer[] childRenderers; // Array to hold the renderers of all children
    private MaterialPropertyBlock propertyBlock;
    public float outlineThickness = 0.0f; // Initial outline thickness
    public float hoverOutlineThickness = 0.013f; // Outline thickness when hovered


    void Start()
    {
        // Get all renderers in the child objects of the parent GameObject
        childRenderers = GetComponentsInChildren<Renderer>();

        // Initialize the MaterialPropertyBlock
        propertyBlock = new MaterialPropertyBlock();

        // Set initial outline thickness for the object
        UpdateOutlineThickness(outlineThickness);
    }

   void OnMouseEnter()
    {
        // Check if a conversation is still active
        if (!ConversationManager.Instance.IsConversationStillActive)
        {
            // Change outline thickness when hovered
            UpdateOutlineThickness(hoverOutlineThickness);
        }
    }

    // When the mouse exits the object (parent object)
    void OnMouseExit()
    {
        // Check if a conversation is still active
        if (!ConversationManager.Instance.IsConversationStillActive)
        {
            // Reset outline thickness to initial value
            UpdateOutlineThickness(outlineThickness);
        }
    }

    private void UpdateOutlineThickness(float thickness)
    {
        // Iterate through all child renderers and apply the outline thickness
        foreach (Renderer renderer in childRenderers)
        {
            // Get the current material properties for the renderer
            renderer.GetPropertyBlock(propertyBlock);

            // Set the outline thickness property in the MaterialPropertyBlock
            propertyBlock.SetFloat("_OutlineThickness", thickness);

            // Apply the updated MaterialPropertyBlock to the renderer
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}
