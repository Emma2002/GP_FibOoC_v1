using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
public class DisplayImage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject imageComponent;
    void Start()
    {
        // Get the Image component and hide the image initially
        imageComponent.gameObject.SetActive(false);
      
    }

    void Update()
    {
        // Check if the boolean has changed and update the image visibility
        bool showImage = ConversationManager.Instance.GetBool("ShowImageOrVideo");
        Debug.Log("Status of the image bool: " + showImage);
        Debug.Log(ConversationManager.Instance.GetBool("ShowImageOrVideo"));
        
        if (ConversationManager.Instance.GetBool("ShowImageOrVideo") == true)
        {
            imageComponent.gameObject.SetActive(true);
        }
        if (ConversationManager.Instance.GetBool("ShowImageOrVideo") == false)
        {
            imageComponent.gameObject.SetActive(false);
        }   
    }
}

