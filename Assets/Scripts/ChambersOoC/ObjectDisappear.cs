using UnityEngine;
using DialogueEditor;

public class ObjectDisappear : MonoBehaviour
{
   [SerializeField]  List<GameObject> disappearingObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject object in disappearingObjects){
        }
    }
}
