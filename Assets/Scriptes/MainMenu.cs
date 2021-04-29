using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject continueBtn, warning;

    private void Start()
    {
        continueBtn = GameObject.FindGameObjectWithTag("ContinueButton");
        warning = GameObject.FindGameObjectWithTag("NewGameWarning");
        if (SaveSystem.LoadPlayer() != null)
        {
            continueBtn.SetActive(true);
        }
        else
        {
            continueBtn.SetActive(false);
        }
        warning.SetActive(false);
    }
    public void ContinueGame()
    {
        if (SaveSystem.LoadPlayer() != null)
        {
            PlayerData data = SaveSystem.LoadPlayer();

            SceneManager.LoadScene(data.scene);
        }
    }

    public void NewGame()
    {
        if (SaveSystem.LoadPlayer() != null)
        {
            warning.SetActive(true);
        }
        else
        {
            StartNewGame();
        }
    }

    public void StartNewGame()
    {
        SaveSystem.DeletePlayer();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
