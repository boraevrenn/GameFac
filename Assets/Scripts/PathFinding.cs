using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] Rigidbody2D enemyRigidbody;
    [SerializeField] Patrol patrol;
    [SerializeField] GameManager gameManager;


    [Header("Player Values")]
    [SerializeField] Vector2 newPlayerPosition;
    [SerializeField] bool isLeftLayerPlayer;
    [SerializeField] bool isRightLayerPlayer;


    [Header("Enemy Values")]
    [SerializeField] float enemySpeed;
    public bool isEnemySeeRight = true;



    [Header("Distance Boolean Values")]
    public bool isPlayerInMaximumDistance;
    public bool isPlayerInMinimumDistance;



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
    [SerializeField] bool isMinimumDistanceTimerSmallerThanZero;
    [SerializeField] bool isMinimumDistanceTimerGreaterThanZero;




    void FixedUpdate()
    {
        if (!gameManager.enterEditMode && !gameManager.gameOver)
        {
            newPlayerPosition = new Vector2(player.transform.position.x, enemyRigidbody.velocity.y);
            enemyAndPlayerDistance = ReturnDistance();
            CalculateAndReturnBooleanFromDistance();
            MoveToPlayer();
            CreateRayForEnemy();
            DefineEnemyDirection();
            isMinimumDistanceTimerSmallerThanZero = timer.ReturnIsMinimumDistanceTimerSmallerZero();
            isMinimumDistanceTimerGreaterThanZero = timer.ReturnIsMinimumDistanceTimerGreaterZero();
            MoveEnemyOpposite();
            DefineRotation();
            RotateEnemyWhenSeePlayer();
        }
    }

    float ReturnDistance()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    void CalculateAndReturnBooleanFromDistance()
    {

        if (enemyAndPlayerDistance <= maximumDistance && enemyAndPlayerDistance > minimumDistance)
        {
            isPlayerInMinimumDistance = false;
            isPlayerInMaximumDistance = true;
        }
        else if (enemyAndPlayerDistance <= minimumDistance)
        {
            isPlayerInMaximumDistance = false;
            isPlayerInMinimumDistance = true;
        }
        else
        {
            isPlayerInMaximumDistance = false;
            isPlayerInMinimumDistance = false;
        }
    }

    public void MoveEnemy(Vector2 position)
    {
        transform.position = Vector2.MoveTowards(transform.position, position, enemySpeed * Time.deltaTime);
    }


    void MoveToPlayer()
    {
        if (isPlayerInMaximumDistance && isMinimumDistanceTimerSmallerThanZero)
        {
            MoveEnemy(newPlayerPosition);
        }
    }

    void CreateRayForEnemy()
    {
        if (isPlayerInMaximumDistance || isPlayerInMinimumDistance)
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
        if (isMinimumDistanceTimerGreaterThanZero && isLeftLayerPlayer)
            MoveEnemy((Vector2)transformForMove.position + moveRight);
        else if (isMinimumDistanceTimerGreaterThanZero && isRightLayerPlayer)
            MoveEnemy((Vector2)transformForMove.position + moveLeft);
    }

    void DefineRotation()
    {
        if (isLeftLayerPlayer)
            isEnemySeeRight = false;
        else if (isRightLayerPlayer)
            isEnemySeeRight = true;
    }

    void RotateEnemyWhenSeePlayer()
    {
        if (!patrol.isPatrolModeEnabled)
        {
            if (isEnemySeeRight)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (!isEnemySeeRight)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}