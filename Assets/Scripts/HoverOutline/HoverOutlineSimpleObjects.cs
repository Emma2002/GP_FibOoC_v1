// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using DialogueEditor;

// public class HoverOutlineSimpleObjects : MonoBehaviour
// {
//     private Transform currentHighlight;  // Stores the currently highlighted object
//     private RaycastHit raycastHit;

//     [SerializeField] 
//     private float outlineWidth = 70.0f;  // Adjustable outline width

//     void Update()
//     {
//         // Disable outline for the previously highlighted object if it's not the current highlight anymore
//         if (currentHighlight != null)
//         {
//             Outline previousOutline = currentHighlight.gameObject.GetComponent<Outline>();
//             if (previousOutline != null)
//             {
//                 previousOutline.enabled = false;
//             }
//             currentHighlight = null;  // Reset the current highlight
//         }

//         // Perform a raycast from the mouse position
//         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//         if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
//         {
//             Transform hitTransform = raycastHit.transform;

//             // Check for a valid target with the required tag
//             if (hitTransform.CompareTag("OutlineSelectSimple") && !ConversationManager.Instance.IsConversationStillActive)
//             {
//                 currentHighlight = hitTransform;  // Set the current highlight
//                 Outline outline = currentHighlight.gameObject.GetComponent<Outline>();

//                 if (outline == null)
//                 {
//                     // Add Outline component if it doesn't already exist
//                     outline = currentHighlight.gameObject.AddComponent<Outline>();
//                 }

//                 // Configure the outline properties
//                 outline.OutlineColor = Color.magenta;
//                 outline.OutlineWidth = outlineWidth;
//                 outline.enabled = true;  // Enable the outline
//             }
//         }
//     }
// }


// using UnityEngine;
// using UnityEngine.EventSystems;
// using DialogueEditor;

// public class HoverOutlineSimpleObjects : MonoBehaviour
// {
//     private Transform currentHighlight;  // Stores the currently highlighted object
//     private RaycastHit raycastHit;

//     [SerializeField]
//     private float outlineWidth = 70.0f;  // Adjustable outline width

//     private bool isConversationActive = false;
//     private bool wasOutlineEnabled = false;  // Track if the outline is currently enabled

//     void Update()
//     {
//         // Check if the conversation state has changed
//         bool currentConversationState = ConversationManager.Instance.IsConversationActive;

//         // Update isConversationActive only if it differs from the current state
//         if (isConversationActive != currentConversationState)
//         {
//             isConversationActive = currentConversationState;
//         }

//         // Disable the outline for the previously highlighted object if conversation is not active
//         if (currentHighlight != null)
//         {
//             Outline previousOutline = currentHighlight.gameObject.GetComponent<Outline>();
//             if (previousOutline != null && !isConversationActive && wasOutlineEnabled)
//             {
//                 previousOutline.enabled = false; // Disable outline only if it was enabled before
//                 wasOutlineEnabled = false; // Track that outline was disabled
//             }
//             currentHighlight = null;  // Reset the highlight
//         }

//         // Perform raycast if the mouse isn't over a UI element and conversation is not active
//         if (!EventSystem.current.IsPointerOverGameObject() && !isConversationActive)
//         {
//             // Get the mouse position in world space for an orthographic camera
//             Vector3 mousePosition = Input.mousePosition;
//             mousePosition.z = Camera.main.nearClipPlane;  // Set the depth (distance from the camera)
//             Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

//             // Raycast from the camera in the direction of the orthographic view
//             Ray ray = new Ray(worldMousePosition, Camera.main.transform.forward);
//             if (Physics.Raycast(ray, out raycastHit))
//             {
//                 Transform hitTransform = raycastHit.transform;

//                 // Check for a valid target with the required tag
//                 if (hitTransform.CompareTag("OutlineSelectSimple"))
//                 {
//                     // Only update the outline if it's a new object or if it's not already highlighted
//                     if (currentHighlight != hitTransform)
//                     {
//                         if (currentHighlight != null)
//                         {
//                             Outline previousOutline = currentHighlight.gameObject.GetComponent<Outline>();
//                             if (previousOutline != null && wasOutlineEnabled)
//                             {
//                                 previousOutline.enabled = false;  // Disable the outline for the previous object
//                             }
//                         }

//                         // Set the current highlight
//                         currentHighlight = hitTransform;
//                         Outline outline = currentHighlight.gameObject.GetComponent<Outline>();

//                         if (outline == null)
//                         {
//                             // Add Outline component if it doesn't already exist
//                             outline = currentHighlight.gameObject.AddComponent<Outline>();
//                         }

//                         // Configure the outline properties
//                         outline.OutlineColor = Color.magenta;
//                         outline.OutlineWidth = outlineWidth;
//                         outline.enabled = true;
//                         wasOutlineEnabled = true; // Mark that the outline was enabled
//                     }
//                 }
//             }
//         }
//     }
// }

using UnityEngine;
using DialogueEditor;

public class HoverOutlineSimpleObjects : MonoBehaviour
{
    [SerializeField]
    private float outlineWidth = 70.0f; // Adjustable outline width

    private Outline outline; // Cached reference to the Outline component
    private bool isConversationActive = false;

    void Start()
    {
        // Cache the Outline component, or add one if it doesn't exist
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
            outline.enabled = false; // Start with the outline disabled
        }

        outline.OutlineColor = Color.magenta; // Configure outline color
        outline.OutlineWidth = outlineWidth; // Configure outline width
    }

    void Update()
    {
        // Update the conversation state
        isConversationActive = ConversationManager.Instance.IsConversationActive;
    }

    void OnMouseEnter()
    {
        // Enable the outline if a conversation is not active
        if (!isConversationActive)
        {
            outline.enabled = true;
        }
    }

    void OnMouseExit()
    {
        // Disable the outline
        outline.enabled = false;
    }
}
