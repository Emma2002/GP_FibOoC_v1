using UnityEngine;
using DialogueEditor;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private string message; // Tooltip message to display

    [SerializeField]
    private Tag objectTag; // The tag associated with this object (via your Tags system)

    void OnMouseEnter()
    {
        // Check if the object or its tag has already been clicked/triggered
             bool isClickedOrTagTriggered =
            ConversationStartAtClick.IsObjectClicked(gameObject) ||
            ConversationStartAtClick.IsTagTriggered(objectTag) ||
            ConversationStartAtCellClick.IsCellClicked(gameObject) ||
            ConversationStartAtCellClick.IsTagTriggered(objectTag) ||
            ConversationStartAtResearchClick.IsResearchClicked(gameObject) ||
            ConversationStartAtResearchClick.IsTagTriggered(objectTag);

        // Show the tooltip only if the object/tag has NOT been triggered and no conversation is active
        if (!isClickedOrTagTriggered && !ConversationManager.Instance.IsConversationActive)
        {
            TooltipManager._instance.SetAndShowToolTip(message);
        }
    }

    void OnMouseExit()
    {
        // Always hide the tooltip when the mouse exits
        TooltipManager._instance.HideTooltip();
    }
}
