using UnityEngine;
using UnityEngine.Video; // Import Video namespace

public class VideoPlayerController : MonoBehaviour
{
    [SerializeField] private string videoFileName; // Name of the video file inside StreamingAssets (e.g., "video.mp4")

    private VideoPlayer videoPlayer; // Reference to the VideoPlayer component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the VideoPlayer component attached to this GameObject
        videoPlayer = GetComponent<VideoPlayer>();

        // Check if the VideoPlayer is attached to the GameObject
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component is missing from the GameObject!");
            return;
        }

        // Construct the full video path using StreamingAssetsPath
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        
        // Check if the video file exists at the specified path
        if (System.IO.File.Exists(videoPath))
        {
            videoPlayer.url = videoPath; // Set the video URL to the full path in StreamingAssets
            videoPlayer.Play(); // Start playing the video
        }
        else
        {
            Debug.LogError($"Video file not found at path: {videoPath}");
        }
    }
}
