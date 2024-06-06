using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    [Header("Vector And Transfor Values")]
    [SerializeField] Vector2 moveRight;
    [SerializeField] Vector2 moveLeft;
    [SerializeField] Transform transformForMove;

    [Header("Components")]
    [SerializeField] Player player;
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Timer timer;



    [Header("Player Values")]
    [SerializeField] Vector2 newPlayerPosition;
    [SerializeField] bool isLeftLayerPlayer;
    [SerializeField] bool isRightLayerPlayer;


    [Header("Enemy Values")]
    [SerializeField] float enemySpeed;



    [Header("Distance Boolean Values")]
    public bool isPlayerInMaximumDistance;
    public bool isPlayerInMinumumDistance;



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


    [Header("Timer Values")]
    [SerializeField] bool isMinumumDistanceTimerSmallerThanZero;



    void FixedUpdate()
    {

        newPlayerPosition = new Vector2(player.transform.position.x, playerRigidbody.velocity.y);
        enemyAndPlayerDistance = ReturnDistance();
        CalculateAndReturnBooleanFromDistance();
        MoveToPlayer();
        CreateRayForEnemy();
        DefineEnemyDirection();
        isMinumumDistanceTimerSmallerThanZero = timer.ReturnIsMinumumDistanceTimerSmallerZero();
        MoveEnemyOpposite();
        EnemyRotationWhenRayHitsPlayer();
    }

    float ReturnDistance()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    void CalculateAndReturnBooleanFromDistance()
    {

        if (enemyAndPlayerDistance <= maximumDistance && enemyAndPlayerDistance > minimumDistance
        && isMinumumDistanceTimerSmallerThanZero)
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


    void MoveToPlayer()
    {

        if (isPlayerInMaximumDistance)
        {
            MoveEnemy(newPlayerPosition);
        }
    }

    void CreateRayForEnemy()
    {
        if (isPlayerInMaximumDistance || isPlayerInMinumumDistance)
        {
            rayForEnemyToSeePlayerLeft = Physics2D.Raycast((Vector2)transform.position + extendRayCastLeft, Vector2.left, rayDistance);
            rayForEnemyToSeePlayerRight = Physics2D.Raycast((Vector2)transform.position + extendRayCastRight, Vector2.right, rayDistance);
        }
    }

    void DefineEnemyDirection()
    {
        if (rayForEnemyToSeePlayerLeft.transform != null)
        {
            isRightLayerPlayer = false;
            isLeftLayerPlayer = LayerMask.LayerToName(rayForEnemyToSeePlayerLeft.transform.gameObject.layer) == "Player";
        }
        else if (rayForEnemyToSeePlayerRight.transform != null)
        {
            isLeftLayerPlayer = false;
            isRightLayerPlayer = LayerMask.LayerToName(rayForEnemyToSeePlayerRight.transform.gameObject.layer) == "Player";
        }
        else
        {
            isRightLayerPlayer = false;
            isLeftLayerPlayer = false;
        }
    }


    void MoveEnemyOpposite()
    {
        if (isPlayerInMinumumDistance && isLeftLayerPlayer)
            MoveEnemy((Vector2)transformForMove.position + moveRight);
        else if (isPlayerInMinumumDistance && isRightLayerPlayer)
            MoveEnemy((Vector2)transformForMove.position + moveLeft);
    }

    void EnemyRotationWhenRayHitsPlayer()
    {
        if (isLeftLayerPlayer)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (isRightLayerPlayer)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }





    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector2.left, Color.black);
        Debug.DrawRay(transform.position, Vector2.right, Color.red);
    }
}