using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SaveManager.Instance.ResetData();

        SceneManager.LoadScene("Game");
    }

    public void LoadGame()
    {
        SaveManager.Instance.Load();

        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
