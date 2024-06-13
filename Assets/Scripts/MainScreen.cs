using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void ExitApplication()
    {
        Application.Quit();
    }
}
