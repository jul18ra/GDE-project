using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameOverScript : MonoBehaviour
{
    public void GameOver()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }

        Time.timeScale = 0;
    }

    public void Retry()
    {
        Scene level1 = SceneManager.GetActiveScene();
        SceneManager.LoadScene(level1.name);
        Time.timeScale = 1;
    }
}
