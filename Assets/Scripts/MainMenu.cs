using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string lvlToLoad = "MainLevel";

    public SceneFader sceneFader;

    public void Play()
    {
        sceneFader.FateTo(lvlToLoad);
    }

    public void Quit()
    {
        Debug.Log("Exciting...");
        Application.Quit();
    }
}
