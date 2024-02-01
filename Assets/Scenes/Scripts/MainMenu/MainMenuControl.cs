using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadScene(1); // 1 is level_0(current gameScene)
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(0); // 0 is MenuScene
    }
}
