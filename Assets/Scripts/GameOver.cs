using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{    
    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;    

    public void Retry()
    {
        sceneFader.FateTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FateTo(menuSceneName);
    }
}
