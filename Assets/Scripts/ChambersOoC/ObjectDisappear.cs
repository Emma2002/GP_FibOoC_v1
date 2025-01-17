using UnityEngine;
using DialogueEditor;
using System.Collections.Generic; // Include this namespace for List<T>

public class ObjectDisappear : MonoBehaviour
{
    [SerializeField] private List<GameObject> disappearingObjects; // Best practice: explicitly declare access modifiers

    // Update is called once per frame
    void Update()
    {
        if (ConversationManager.Instance.GetBool("ECMDialogueFinished") == true)
        {
            foreach (GameObject obj in disappearingObjects) // Changed 'object' to 'obj'
            {
                obj.SetActive(false);
            }
        }
    }
}
