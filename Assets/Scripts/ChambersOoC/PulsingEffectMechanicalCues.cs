using UnityEngine;

public class PulsingEffectMechanicalCues : MonoBehaviour
{
    [SerializeField] public Transform targetObject; // The object you want to pulse
    [SerializeField] public float pulseIntensity = 0.02f; // How much larger/smaller the object becomes
    [SerializeField] public float pulseSpeed = 2.0f; // Speed of the pulsing effect

    private Vector3 originalScale; // Store the object's original scale
    private bool isPulsing = false; // Whether the object is currently pulsing

    void Start()
    {
        if (targetObject == null)
        {
            targetObject = transform; // Default to the object this script is attached to
        }

        originalScale = targetObject.localScale;
    }

    void Update()
    {
        if (isPulsing)
        {
            // Calculate the pulsing scale using a sine wave
            float scaleFactor = 1.0f + Mathf.Sin(Time.time * pulseSpeed) * pulseIntensity;
            targetObject.localScale = originalScale * scaleFactor; // Scale proportionally to the original size
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Replace "Player" with the tag you want to detect
        {
            isPulsing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPulsing = false;
            targetObject.localScale = originalScale; // Reset to the original scale
        }
    }
}