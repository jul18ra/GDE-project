using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private int mainMenu = 0;
    private int levelScene = 1;
    private int gameOver = 2;
    public GameObject controlInfoPanel;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(levelScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenControls()
    {
        controlInfoPanel.SetActive(true);
    }

    public void CloseControls()
    {
        controlInfoPanel.SetActive(false);
    }
}
