using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button[] buttonToDisable;
    [SerializeField]
    private GameObject darkPanel;
    public void OnGamePanel()
    {
        gameObject.SetActive(true);
        darkPanel.SetActive(true);
        foreach (Button button in buttonToDisable)
        {
            button.interactable = false;
        }
        Time.timeScale = 0f;
    }

    public void OffPanel()
    {
        gameObject.SetActive(false);
        darkPanel.SetActive(false);
        foreach (Button button in buttonToDisable)
        {
            button.interactable = true;
        }
        Time.timeScale = 1f;
    }
}
