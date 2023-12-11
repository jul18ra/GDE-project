using UnityEngine;
using UnityEngine.SceneManagement;

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
