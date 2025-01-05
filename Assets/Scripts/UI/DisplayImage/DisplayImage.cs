using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
public class DisplayImage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject imageComponent;
    [SerializeField] private string startVideoBool;
    [SerializeField] private GameObject dialogueScreen;

    [SerializeField] private float yPositionChange;

    private float originalFloatY;
    void Start()
    {
        // Get the Image component and hide the image initially
        imageComponent.gameObject.SetActive(false);

        originalFloatY = dialogueScreen.transform.position.y;
        // Debug.Log("The original Float issssssss: " + originalFloatY);
        
        // SetDialogueScreenPosition(false);
      
    }

    void Update()
    {
        // Check if the boolean has changed and update the image visibility
        bool showImage = ConversationManager.Instance.GetBool(startVideoBool);
        // Debug.Log("Status of the image bool: " + showImage);
        // Debug.Log(ConversationManager.Instance.GetBool(startVideoBool));
        
        if (ConversationManager.Instance.GetBool(startVideoBool) == true)
        {
            imageComponent.gameObject.SetActive(true);
            dialogueScreen.transform.position = new Vector3(dialogueScreen.transform.position.x, originalFloatY + yPositionChange, dialogueScreen.transform.position.z);

        }
        if (ConversationManager.Instance.GetBool(startVideoBool) == false)
        {
            imageComponent.gameObject.SetActive(false);
            dialogueScreen.transform.position = new Vector3(dialogueScreen.transform.position.x, originalFloatY, dialogueScreen.transform.position.z);

        }   
    }

}