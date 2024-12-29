using UnityEngine;

[CreateAssetMenu(fileName = "AnsweredCorrectCountSO", menuName = "Scriptable Objects/AnsweredCorrectCountSO")]
public class AnsweredCorrectCountSO : ScriptableObject
{
    [SerializeField] private int AnsweredCorrectCount;

    public int AnsweredCorrectCountScore{
        get { return AnsweredCorrectCount;}
        set { AnsweredCorrectCount = value;}
    }
}
