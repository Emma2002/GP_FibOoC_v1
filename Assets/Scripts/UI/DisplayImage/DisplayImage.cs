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
        // Initially hide the image and store the original dialogue screen position
        imageComponent.gameObject.SetActive(false);
        originalFloatY = dialogueScreen.transform.position.y;
    }

    void Update()
    {
        // Only update when the state of the imageComponent needs to change
        if (ConversationManager.Instance.GetBool(startVideoBool))
        {
            if (!imageComponent.activeSelf) // Only activate if not already active
            {
                imageComponent.gameObject.SetActive(true);
                dialogueScreen.transform.position = new Vector3(
                    dialogueScreen.transform.position.x,
                    originalFloatY + yPositionChange,
                    dialogueScreen.transform.position.z
                );
            }
        }
        else
        {
            if (imageComponent.activeSelf) // Only deactivate if already active
            {
                imageComponent.gameObject.SetActive(false);
                dialogueScreen.transform.position = new Vector3(
                    dialogueScreen.transform.position.x,
                    originalFloatY,
                    dialogueScreen.transform.position.z
                );
            }
        }
    }
}