using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public bool enterEditMode;
   [SerializeField] Player player;
    [SerializeField] GameObject editPanel;
    public bool gameOver;
    [Header("Background")]
    [SerializeField] Material editBackgroundMaterial;
    [SerializeField] float backgroundSpeed;

    [Header("Values")]
    [SerializeField] float cameraSpeed;

    private void Awake()
    {
        GameManagerSingleton();
        if (player == null)
            player = FindObjectOfType<Player>();
       
      
    }
   
    
    private void Update()
    {
        if(editPanel == null)
        {
            editPanel = GameObject.FindGameObjectWithTag("Panel");
        }

        ParallaxEffect();
        EnterEditMode();
        GameOver();
        InEditModeMoveCamera();
    }
   
    
    
    void EnterEditMode()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!enterEditMode)
            {
                enterEditMode = true;
                editPanel.gameObject.SetActive(true);
            }
            else
            {
                enterEditMode = false;
                editPanel.gameObject.SetActive(false);
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
        if (player != null)
        {
            if (player.health <= Mathf.Epsilon)
            {
                gameOver = true;
            }
        }

    }

    void ParallaxEffect()
    {
        if (!gameOver)
        {
            editBackgroundMaterial.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0);
        }
    }

    void InEditModeMoveCamera()
    {
        if (enterEditMode)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");
            Camera.main.transform.position += new Vector3(horizontalMovement * cameraSpeed * Time.deltaTime, verticalMovement * cameraSpeed * Time.deltaTime, 0);
        }
    }
}
