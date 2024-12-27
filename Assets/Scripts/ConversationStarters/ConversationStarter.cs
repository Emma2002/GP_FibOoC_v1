using UnityEngine;
using DialogueEditor;
using TMPro;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // private int AnsweredCorrectCount;

    void Start()
    {
        ConversationManager.Instance.StartConversation(myConversation);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
