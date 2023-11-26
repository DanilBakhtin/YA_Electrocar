using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private Text textTimeFinish;
    [SerializeField] private GameObject sounds;

    private bool isPause;
    private bool isFinish;
    private bool isFail;

    void Start()
    {
        isPause = false;
        pausePanel.SetActive(isPause);
        Time.timeScale = 1;

        isFail = false;
        failPanel.SetActive(isFail);

        isFinish = false;
        finishPanel.SetActive(isFinish);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause && !isFinish && !isFail)
        {
            pauseGame();
        }
    }
    public void ContinueGame()
    {
        isPause = false;
        pausePanel.SetActive(isPause);
        Time.timeScale = 1;
        playSounds();
    }

    public void pauseGame()
    {
        isPause = true;
        pausePanel.SetActive(isPause);
        Time.timeScale = 0;
        stopSounds();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void toMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void nextLevel()
    {

        if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
        {
            toMainMenu();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void finishTrace(float time)
    {
        Time.timeScale = 0;
        isFinish = true;
        finishPanel.SetActive(true);
        textTimeFinish.text = time.ToString("F2") + " s";
        stopSounds();

    }
    public void failTrace()
    {
        Time.timeScale = 0;
        isFail = true;
        failPanel.SetActive(true);
        stopSounds();
    }

    private void stopSounds()
    {
        AudioListener.pause = true;
    }

    public void playSounds()
    {
        AudioListener.pause = false;
    }
}
