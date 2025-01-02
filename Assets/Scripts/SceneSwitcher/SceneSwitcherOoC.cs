using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DialogueEditor;


public class SceneSwitcherOoC : MonoBehaviour
{
      [SerializeField] private NPCConversation myConversation;
    // Update is called once per frame
    void Update()
    {
        if(ConversationManager.Instance.GetBool("SwitchScene"))
        {
            ChangeScene();
        }
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("03_Level2OrganOnChip");
    }
}
