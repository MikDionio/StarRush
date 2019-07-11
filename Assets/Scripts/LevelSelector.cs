using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    private Scene currentScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(currentScene.name);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void loadLevelOne()
    {
        SceneManager.LoadScene("Level1");
    }

    public void loadLevelTwo()
    {
        SceneManager.LoadScene("Level2");
    }

    public void loadNextLevel()
    {
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }
}
