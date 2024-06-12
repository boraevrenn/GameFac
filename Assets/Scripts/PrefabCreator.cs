using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabCreator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;

    [Header("Bool Values")]
    [SerializeField] bool isCreateEnemy;
    [SerializeField] bool isCreatePlayer;


    [Header("Images")]
    [SerializeField] Sprite playerImage;
    [SerializeField] Sprite enemyImage;

    Vector3 mousePosition;
    Vector3 mousePositionWorldPoint;

    private void Update()
    {
        //TakeMousePosition();
        //CreateEnemy();
    }

    public void IfButtonClickedCreateEnemy()
    {
        isCreateEnemy = !isCreateEnemy;
    }

    public void IfButtonClickedCreatePlayer()
    {
        isCreatePlayer = !isCreatePlayer;
    }

    void TakeMousePosition()
    {
        mousePosition = Input.mousePosition;
        mousePositionWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void CreateEnemy()
    {
        if (isCreateEnemy) 
        {
            Instantiate(playerImage,mousePositionWorldPoint, Quaternion.identity);
        }
    }
}
