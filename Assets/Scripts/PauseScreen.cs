using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;


    private void Update()
    {
        OpenClosePausePanel();
    }
    void OpenClosePausePanel()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            bool pausePanelState = !pausePanel.activeSelf;
            pausePanel.SetActive(pausePanelState);
        }
    }

    public void ExitMainMenu()
    {
        pausePanel.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
