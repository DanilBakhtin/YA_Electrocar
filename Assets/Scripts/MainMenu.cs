using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void play(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
