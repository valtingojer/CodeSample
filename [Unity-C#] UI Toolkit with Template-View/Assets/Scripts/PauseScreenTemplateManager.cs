using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenTemplateManager : MonoBehaviour
{
    [SerializeField] GameObject PauseScreen;
    bool isPaused = false;

    GameObject FreshScreen => Instantiate(PauseScreen, Vector3.zero, Quaternion.identity, gameObject.transform);

    GameObject currentScreen;

    void PauseGame()
    {
        //currentScreen ??= FreshScreen;
        currentScreen = FreshScreen;
        currentScreen.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    void ResumeGame()
    {
        try
        {
            currentScreen?.SetActive(false);
            currentScreen = null;
            Time.timeScale = 1;
            isPaused = false;
        }
        catch (System.Exception ex)
        {
            PauseGame();
        }
        
    }

    void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void Awake()
    {
        ResumeGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}
