using UnityEngine;
using UnityEngine.Rendering;

public class DisableFogForMinimap : MonoBehaviour
{
    private bool originalFogState;

    private void OnEnable()
    {
        // Subscribe to the rendering events
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    private void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera.name == "MinimapCamera") 
        {
            Debug.Log($"Disabling fog for camera: {camera.name}");
            originalFogState = RenderSettings.fog;
            RenderSettings.fog = false;
        }
    }

    private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera.name == "MinimapCamera") // Replace with the name of your minimap camera
        {
            Debug.Log($"Restoring fog for camera: {camera.name}");
            RenderSettings.fog = originalFogState;
        }
    }
}
