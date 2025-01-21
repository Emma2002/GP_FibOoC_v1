using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
using UnityEngine.Video; // Import Video namespace

public class DisplayImage : MonoBehaviour
{
    [SerializeField] private GameObject imageComponent;
    [SerializeField] private string startVideoBool;
    [SerializeField] private GameObject dialogueScreen;

    [SerializeField] private float yPositionChange;

    [SerializeField] private VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    [SerializeField] private string videoFileName; // Name of the video file (e.g., "myVideo.mp4")

    private float originalFloatY;

    void Start()
    {
        // Initially hide the image and store the original dialogue screen position
        imageComponent.gameObject.SetActive(false);
        originalFloatY = dialogueScreen.transform.localPosition.y;

        // Set the videoPlayer's URL to the video file in StreamingAssets
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
    }

    void Update()
    {
        // Only update when the state of the imageComponent needs to change
        if (ConversationManager.Instance.GetBool(startVideoBool))
        {
            if (!imageComponent.activeSelf) // Only activate if not already active
            {
                imageComponent.gameObject.SetActive(true);
                dialogueScreen.transform.localPosition = new Vector3(
                    dialogueScreen.transform.localPosition.x,
                    originalFloatY + yPositionChange,
                    dialogueScreen.transform.localPosition.z
                );

                // Play the video
                if (!videoPlayer.isPlaying)
                {
                    videoPlayer.Play();
                }
            }
        }
        else
        {
            if (imageComponent.activeSelf) // Only deactivate if already active
            {
                imageComponent.gameObject.SetActive(false);
                dialogueScreen.transform.localPosition = new Vector3(
                    dialogueScreen.transform.localPosition.x,
                    originalFloatY,
                    dialogueScreen.transform.localPosition.z
                );

                // Stop the video
                if (videoPlayer.isPlaying)
                {
                    videoPlayer.Stop();
                }
            }
        }
    }
}
