using UnityEngine;
using TMPro;
using DialogueEditor;

public class DisplayScoreCountEndScreen : MonoBehaviour
{
    [SerializeField] private AnsweredCorrectCountSO answeredCorrectCountSO;
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
      UpdateScoreText();
    }
    // Update is called once per frame
    void Update()
    {
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Your Score:\n{answeredCorrectCountSO.AnsweredCorrectCountScore}";
        }
    }
}
