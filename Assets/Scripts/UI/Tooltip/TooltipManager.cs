using UnityEngine;
using TMPro;
using UnityEngine.Experimental.GlobalIllumination;
using System;
using DialogueEditor;

public class TooltipManager : MonoBehaviour
{
   public static TooltipManager _instance;
   public TextMeshProUGUI textComponent;

   private void Awake() {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else{
            _instance = this;
        }
   }

   private void Start() {
    Cursor.visible = true;
    gameObject.SetActive(false); 
   }

   private void Update() {
    transform.position = Input.mousePosition;
   }

   public void SetAndShowToolTip(string message)
   {
    gameObject.SetActive(true);
    textComponent.text = message;
   }

   public void HideTooltip()
   {
    gameObject.SetActive(false);
    textComponent.text = String.Empty;
   }
}
