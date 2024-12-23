using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("Level1FibrosisExplanation");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
