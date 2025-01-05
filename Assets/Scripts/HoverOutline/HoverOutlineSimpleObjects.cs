using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DialogueEditor;

public class HoverOutlineSimpleObjects : MonoBehaviour
{
    private Transform currentHighlight;  // Stores the currently highlighted object
    private RaycastHit raycastHit;

    [SerializeField] 
    private float outlineWidth = 70.0f;  // Adjustable outline width

    void Update()
    {
        // Disable outline for the previously highlighted object if it's not the current highlight anymore
        if (currentHighlight != null)
        {
            Outline previousOutline = currentHighlight.gameObject.GetComponent<Outline>();
            if (previousOutline != null)
            {
                previousOutline.enabled = false;
            }
            currentHighlight = null;  // Reset the current highlight
        }

        // Perform a raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            Transform hitTransform = raycastHit.transform;

            // Check for a valid target with the required tag
            if (hitTransform.CompareTag("OutlineSelectSimple") && !ConversationManager.Instance.IsConversationStillActive)
            {
                currentHighlight = hitTransform;  // Set the current highlight
                Outline outline = currentHighlight.gameObject.GetComponent<Outline>();

                if (outline == null)
                {
                    // Add Outline component if it doesn't already exist
                    outline = currentHighlight.gameObject.AddComponent<Outline>();
                }

                // Configure the outline properties
                outline.OutlineColor = Color.magenta;
                outline.OutlineWidth = outlineWidth;
                outline.enabled = true;  // Enable the outline
            }
        }
    }
}
