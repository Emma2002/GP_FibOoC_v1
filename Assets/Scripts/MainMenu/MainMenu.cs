using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // Loads the first scene
    public void PlayGame(){
        SceneManager.LoadScene("02_Level1FibrosisExplanation");
    }

    // Loads the main menu screen
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("01_MenuScreen");
    }

    // Closes the application
    public void QuitGame()
    {
        Application.Quit();
    }
}
