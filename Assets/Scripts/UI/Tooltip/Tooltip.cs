using UnityEngine;
using DialogueEditor;

public class Tooltip : MonoBehaviour
{
   public string message;

   void OnMouseEnter() {
     if (!ConversationManager.Instance.IsConversationStillActive)
        {
    TooltipManager._instance.SetAndShowToolTip(message);
        }
   }

    void OnMouseExit() {
        TooltipManager._instance.HideTooltip();     
    }
}
