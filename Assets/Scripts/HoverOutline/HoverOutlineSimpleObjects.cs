using UnityEngine;
using DialogueEditor;

public class HoverOutlineSimpleObjects : MonoBehaviour
{
    [SerializeField]
    private float outlineWidth = 70.0f; // Adjustable outline width

    private Outline outline; // Cached reference to the Outline component

    [SerializeField]
    private Tag objectTag; // Tag associated with this object (via custom Tags system)

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

    void OnMouseEnter()
    {
        // Check if the object has been clicked or its tag has been triggered
        bool isClickedOrTagTriggered =
            ConversationStartAtClick.IsObjectClicked(gameObject) ||
        ConversationStartAtClick.IsTagTriggered(objectTag) ||
        ConversationStartAtCellClick.IsCellClicked(gameObject) ||
        ConversationStartAtCellClick.IsTagTriggered(objectTag) ||
        ConversationStartAtResearchClick.IsResearchClicked(gameObject) ||
        ConversationStartAtResearchClick.IsTagTriggered(objectTag);

        // Disable outline if the object/tag has been triggered or a conversation is active
        if (isClickedOrTagTriggered || ConversationManager.Instance.IsConversationActive)
        {
            outline.enabled = false;
        }
        else
        {
            outline.enabled = true; // Enable the outline otherwise
        }
    }

    void OnMouseExit()
    {
        // Always disable the outline when the mouse exits
        outline.enabled = false;
    }
}
