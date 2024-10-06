using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private GameManager gamePause;
    [SerializeField]
    private ObjectDetector detector;
    [SerializeField]
    private GameManager gameOver;



    public void StartButton()
    {
        SceneManager.LoadScene("Stage");
        Time.timeScale = 1.0f;
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("Start");
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void Resume()
    {
        gamePause.OffPanel();
        detector.isPause = false;
    }

}
