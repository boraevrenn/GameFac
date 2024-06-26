using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    [Header("Componenets")]
    [SerializeField] PathFinding pathfinding;
    [SerializeField] Timer timer;
    [SerializeField] GameManager gameManager;
    [Header("Patrol Values")]
    public bool isPatrolModeEnabled;
    float patrolTimer;
    float changeDirectionTimer;

    [Header("Position Values")]
    [SerializeField] Transform enemyTransform;
    [SerializeField] Vector2 moveRightAndLeft;
    [SerializeField] bool changeMovementAndRotationDirection;
    [SerializeField] float addToRotationAndMovementTimer;
    [SerializeField] float rotationX;


    private void Awake()
    {
        if(pathfinding == null)
            pathfinding = GetComponent<PathFinding>();
        if(timer == null)
            timer = GetComponent<Timer>();
        if(gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        if(!gameManager.enterEditMode && !gameManager.gameOver)
        {
            patrolTimer = timer.startPatrolTimer;
            DefinePatrol();
            StartPatrol();
            changeDirectionTimer = timer.enemyDirectionTimer;
            DefineMovementAndRotationDirection();
            ChangeMovementDirection();
            ChangeRotation();
        }
    }


    void DefinePatrol()
    {
        if (patrolTimer < Mathf.Epsilon)
        {
            isPatrolModeEnabled = true;
        }
        else
        {
            isPatrolModeEnabled = false;
        }
    }

    void StartPatrol()
    {
        if (isPatrolModeEnabled)
        {
            pathfinding.MoveEnemy((Vector2)enemyTransform.position + moveRightAndLeft);
        }
    }

    void DefineMovementAndRotationDirection()
    {
        if (isPatrolModeEnabled)
        {
            if (changeDirectionTimer <= Mathf.Epsilon + addToRotationAndMovementTimer)
                changeMovementAndRotationDirection = true;
            else
                changeMovementAndRotationDirection = false;
        }
    }

    void ChangeMovementDirection()
    {
        if (changeMovementAndRotationDirection)
            moveRightAndLeft = -moveRightAndLeft;
    }

    void ChangeRotation()
    {
        if (changeMovementAndRotationDirection)
        {
            rotationX = -rotationX;
            transform.localScale = new Vector2(rotationX, 1);
        }
    }

}
