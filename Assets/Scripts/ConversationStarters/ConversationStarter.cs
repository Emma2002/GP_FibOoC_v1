using UnityEngine;
using DialogueEditor;
using TMPro;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int AnsweredCorrectCount;
    private bool HasAnsweredCorrect = false;
    [SerializeField] private TextMeshProUGUI scoreText;
    void Start()
    {
        ConversationManager.Instance.StartConversation(myConversation);
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if(ConversationManager.Instance.GetBool("AnsweredCorrect") == true && !HasAnsweredCorrect)
        {
            AnsweredCorrectCount += 1;
            HasAnsweredCorrect = true;
            UpdateScoreText();
        }
        if(ConversationManager.Instance.GetBool("AnsweredCorrect") == false)
        {
            HasAnsweredCorrect = false;
            UpdateScoreText();
        }
    }

     private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {AnsweredCorrectCount}";
        }
    }
}
