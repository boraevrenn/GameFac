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
    [Header("Background")]
    [SerializeField] Material editBackgroundMaterial;
    [SerializeField] float backgroundSpeed;

    private void Awake()
    {
        if(player == null)
            player = FindObjectOfType<Player>();
        GameManagerSingleton();
    }
    private void Update()
    {
        ParallaxEffect();
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
    
    void ParallaxEffect()
    {
        if(!gameOver)
        {
            editBackgroundMaterial.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0);
        }
    }
}
