using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PrefabCreator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject playerConfigPrefab;
    [SerializeField] Transform panelPosition;
    [SerializeField] GameManager gameManager;





    [SerializeField] Player playerC;
    [Header("Bool Values")]
    [SerializeField] bool isCreateEnemy;
    [SerializeField] bool isCreatePlayer;
    [SerializeField] bool isRemovePrefabTrue;

    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] SpriteRenderer enemySprite;


    Vector3 mousePosition;
    Vector3 mousePositionToWorldPoint;


    private void Awake()
    {
        gameManager =FindObjectOfType<GameManager>();
        playerSprite = GameObject.FindGameObjectWithTag("PlayerSprite").ConvertTo<SpriteRenderer>();
        enemySprite = GameObject.FindGameObjectWithTag("EnemySprite").ConvertTo<SpriteRenderer>();
    }


    private void Update()
    {
        TakeMousePosition();
        MoveSprite();
        InstantiatePrefab();
        RemovePrefab();
        OpenClosePlayerConfig();
    }

    public void IfButtonClickedCreateEnemy()
    {
        isCreateEnemy = !isCreateEnemy;
        if (isCreateEnemy)
        {
            enemySprite.enabled = true;
            playerSprite.enabled = false;
            isCreatePlayer = false;
            isRemovePrefabTrue = false;
        }

    }

    public void IfButtonClickedCreatePlayer()
    {
        isCreatePlayer = !isCreatePlayer;
        if (isCreatePlayer)
        {
            playerSprite.enabled = true;
            enemySprite.enabled = false;
            isCreateEnemy = false;
            isRemovePrefabTrue = false;
        }

    }

    public void IfRemovePrefabClicked()
    {
        isRemovePrefabTrue = !isRemovePrefabTrue;

        if (isCreateEnemy)
        {
            enemySprite.enabled = false;
            isCreateEnemy = false;

        }

        if (isCreatePlayer)
        {
            playerSprite.enabled = false;
            isCreatePlayer = false;
        }

    }

    void TakeMousePosition()
    {
        mousePosition = Input.mousePosition;
        mousePositionToWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void MoveSprite()
    {
        if (isCreateEnemy)
        {
            enemySprite.transform.position = new Vector3(mousePositionToWorldPoint.x, mousePositionToWorldPoint.y, 0);
        }

        if (isCreatePlayer)
        {
            playerSprite.transform.position = new Vector3(mousePositionToWorldPoint.x, mousePositionToWorldPoint.y, 0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            isCreateEnemy = false;
            enemySprite.enabled = false;
            isCreatePlayer = false;
            playerSprite.enabled = false;
        }
    }


    void InstantiatePrefab()
    {
        if (isCreateEnemy)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.currentSelectedGameObject)
            {
                Instantiate(enemy, new Vector3(mousePositionToWorldPoint.x, mousePositionToWorldPoint.y, 0), Quaternion.identity, transform);
            }
        }

        if (isCreatePlayer)
        {
            if (FindObjectsOfType<Player>().Length < 1)
            {
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.currentSelectedGameObject)
                {
                    playerC = Instantiate(player, new Vector3(mousePositionToWorldPoint.x, mousePositionToWorldPoint.y, 0), Quaternion.identity, transform).ConvertTo<Player>();
                    playerC.playerInfoCanvas = Instantiate(playerConfigPrefab, panelPosition.position, Quaternion.identity);
                }
            }
        }
    }

    void OpenClosePlayerConfig()
    {
        RaycastHit2D ray = Physics2D.Raycast(mousePositionToWorldPoint, Vector3.forward, 300);
        if (Input.GetMouseButton(0))
        {
            if (ray)
            {
                if (ray.transform.tag == "Player")
                {
                    if (playerC.playerInfoCanvas != null)
                    {
                        if (!playerC.playerInfoCanvas.activeSelf && gameManager.enterEditMode)
                        {
                          playerC.playerInfoCanvas.SetActive(true);
                        }
                    }
                }

            }
        }

        if (Input.GetMouseButton(1))
        {
            if (playerC != null)
            {
                if (playerC.playerInfoCanvas != null)
                   playerC.playerInfoCanvas.SetActive(false);
            }
        }

    }

        void RemovePrefab()
        {
            if (isRemovePrefabTrue)
            {
                if (Input.GetMouseButton(0))
                {
                    RaycastHit2D ray = Physics2D.Raycast(mousePositionToWorldPoint, Vector3.forward, 5);
                    if (ray)
                    {
                        if (ray.transform.gameObject.tag == "Enemy")
                        {
                            Destroy(ray.transform.gameObject.GetComponent<Enemy>().enemyHealthCanvas);
                            Destroy(ray.transform.gameObject);
                        }
                        if (ray.transform.gameObject.tag == "Player")
                        {
                            Destroy(ray.transform.gameObject.GetComponent<Player>().playerHealthCanvas);
                            Destroy(ray.transform.gameObject);
                        }
                    }
                }
                if (Input.GetMouseButton(1))
                {
                    isRemovePrefabTrue = false;
                }
            }
        }
    

}
