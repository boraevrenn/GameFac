using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public bool enterEditMode;
    [SerializeField] Player player;
    public bool gameOver;


    private void Awake()
    {
        GameManagerSingleton();
    }
    private void Update()
    {
        EnterEditMode();
        GameOver();
    }
    void EnterEditMode()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!enterEditMode)
            {
                enterEditMode = true;
            }
            else
            {
                enterEditMode = false;
            }


        }
    }

    void GameManagerSingleton()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    void GameOver()
    {
        if(player!=null)
        {
            if (player.health <= Mathf.Epsilon)
            {
                gameOver = true;
            }
        }
     
    }
}
