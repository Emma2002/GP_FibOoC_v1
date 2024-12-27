using UnityEngine;
using TMPro;
using DialogueEditor;
public class DisplayScoreCount2 : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private AnsweredCorrectCountSO answeredCorrectCountSO;
    [SerializeField] private TextMeshProUGUI scoreText;
    private bool HasAnsweredCorrect = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
        
    }

    // Update is called once per frame
    void Update()
    {
         if(ConversationManager.Instance.GetBool("AnsweredCorrect") == true && !HasAnsweredCorrect)
        {
            answeredCorrectCountSO.AnsweredCorrectCountScore += 1;
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
            scoreText.text = $"Score: {answeredCorrectCountSO.AnsweredCorrectCountScore}";
        }
    }
}
