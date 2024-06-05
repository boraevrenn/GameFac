using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    [Header("Vector And Transfor Values")]
    [SerializeField] Vector2 moveRight;
    [SerializeField] Vector2 moveLeft;
    [SerializeField] Transform transformForRun;

    [Header("Components")]
    [SerializeField] Player player;
    [SerializeField] Rigidbody2D playerRigidbody;



    [Header("Player Values")]
    [SerializeField] Vector2 newPlayerPosition;


    [Header("Enemy Values")]
    [SerializeField] float enemySpeed;



    [Header("Distance Boolean Values")]
    [SerializeField] bool isPlayerInMaximumDistance;
    [SerializeField] bool isPlayerInMinumumDistance;



    [Header("Distance Values")]
    [SerializeField] float enemyAndPlayerDistance;
    [SerializeField] float maximumDistance;
    [SerializeField] float minimumDistance;



    [Header("Ray Values")]
    [SerializeField] RaycastHit2D rayForEnemyToSeePlayerLeft;
    [SerializeField] RaycastHit2D rayForEnemyToSeePlayerRight;
    [SerializeField] Vector2 extendRayCastLeft;
    [SerializeField] Vector2 extendRayCastRight;
    [SerializeField] float rayDistance;





    void FixedUpdate()
    {
        newPlayerPosition = new Vector2(player.transform.position.x, playerRigidbody.velocity.y);
        enemyAndPlayerDistance = ReturnDistance();
        CalculateAndReturnBooleanFromDistance();
        CreateRayForEnemy();
        DefineDirection();
    }

    float ReturnDistance()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    void CalculateAndReturnBooleanFromDistance()
    {
        if (enemyAndPlayerDistance <= maximumDistance && enemyAndPlayerDistance > minimumDistance)
        {
            isPlayerInMinumumDistance = false;
            isPlayerInMaximumDistance = true;
        }
        else if (enemyAndPlayerDistance <= minimumDistance)
        {
            isPlayerInMaximumDistance = false;
            isPlayerInMinumumDistance = true;
        }
        else
        {
            isPlayerInMaximumDistance = false;
            isPlayerInMinumumDistance = false;
        }
    }

    void MoveEnemy(Vector2 position)
    {
        transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);
    }


    void CreateRayForEnemy()
    {
        if (isPlayerInMaximumDistance || isPlayerInMinumumDistance)
        {
            rayForEnemyToSeePlayerLeft = Physics2D.Raycast((Vector2)transform.position + extendRayCastLeft, Vector2.left, rayDistance);
            rayForEnemyToSeePlayerRight = Physics2D.Raycast((Vector2)transform.position + extendRayCastRight, Vector2.right, rayDistance);
            Debug.Log(rayForEnemyToSeePlayerLeft.transform.name);
            Debug.Log(rayForEnemyToSeePlayerRight.transform.name);
        }
    }


    void DefineDirection()
    {
        if (isPlayerInMinumumDistance && LayerMask.LayerToName(rayForEnemyToSeePlayerLeft.transform.gameObject.layer) == "Player")
        {
            MoveEnemy((Vector2)transformForRun.position + moveRight);
        }
    }





    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector2.left, Color.black);
        Debug.DrawRay(transform.position, Vector2.right, Color.red);
    }
}