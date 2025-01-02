using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DialogueEditor;

public class HoverOutlineSimpleObjects : MonoBehaviour
{
    private Transform highlight;
    private RaycastHit raycastHit;

    [SerializeField] 
    private float outlineWidth = 70.0f;  // This will allow you to adjust the outline width from the Inspector

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) // Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("OutlineSelectSimple") && highlight != null && !ConversationManager.Instance.IsConversationStillActive) // Check if the conversation is over
            {
                Outline outline = highlight.gameObject.GetComponent<Outline>();
                
                if (outline != null)
                {
                    outline.enabled = true;
                }
                else
                {
                    outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                }

                // Always update OutlineWidth when highlight changes
                outline.OutlineColor = Color.magenta;
                outline.OutlineWidth = outlineWidth;  // Use the SerializeField outlineWidth
            }
            else
            {
                highlight = null;
            }
        }
    }
}
