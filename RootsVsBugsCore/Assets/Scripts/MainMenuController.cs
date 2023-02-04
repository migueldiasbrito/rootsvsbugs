using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene("SinglePlayerGameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
