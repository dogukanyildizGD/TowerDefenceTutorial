using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toogle();
        }
    }

    public void Toogle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Continue()
    {
        Toogle();
    }

    public void Retry()
    {
        Toogle();
        sceneFader.FateTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toogle();
        sceneFader.FateTo(menuSceneName);
    }
}
