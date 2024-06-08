using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] PathFinding pathFinding;
    [SerializeField] Patrol patrol;
    [SerializeField] bool isEnemy;
    [Header("Player In Minimum Distance Run Timer Values")]
    [SerializeField] float waitMinimumDistanceTimer;
    [SerializeField] float waitMinimumDistanceTotalTime;

    [Header("Patrol Values")]
    public float startPatrolTimer;
    [SerializeField] float startPatrolTimerTotal;
    [SerializeField] bool workForOneTime;


    [Header("In Patrol Direction Timer")]
    public float enemyDirectionTimer;
    [SerializeField] float enemyDirectionTimerTotal;



    void FixedUpdate()
    {
        if (isEnemy)
        {
            waitForReturnEnemyToMinimumDistance();
            StartPatrolTimer();
            ChangeMovementAndRotationDirectionTimer();    
        }
     
    }

    //Walk To Maximum Distance Timer
    void waitForReturnEnemyToMinimumDistance()
    {
        waitMinimumDistanceTimer -= Time.deltaTime;
        if (pathFinding.isPlayerInMinimumDistance && waitMinimumDistanceTimer <= Mathf.Epsilon)
        {
            waitMinimumDistanceTimer = waitMinimumDistanceTotalTime;
        }
    }

    public bool ReturnIsMinimumDistanceTimerSmallerZero()
    {
        return waitMinimumDistanceTimer <= Mathf.Epsilon;
    }

    public bool ReturnIsMinimumDistanceTimerGreaterZero()
    {
        return waitMinimumDistanceTimer > Mathf.Epsilon;
    }

    //Start Patrol Timer
    void StartPatrolTimer()
    {
        startPatrolTimer -= Time.deltaTime;
        if (startPatrolTimer <= Mathf.Epsilon && workForOneTime && !pathFinding.isPlayerInMaximumDistance && !pathFinding.isPlayerInMinimumDistance)
        {
            workForOneTime = false;
            startPatrolTimer = startPatrolTimerTotal;
        }
        else if (pathFinding.isPlayerInMaximumDistance || pathFinding.isPlayerInMinimumDistance)
        {
            workForOneTime = true;
            startPatrolTimer = Mathf.Epsilon;
        }
    }

    void ChangeMovementAndRotationDirectionTimer()
    {
        enemyDirectionTimer -= Time.deltaTime;
        if (patrol.isPatrolModeEnabled && enemyDirectionTimer <= Mathf.Epsilon)
        {
            enemyDirectionTimer = enemyDirectionTimerTotal;
        }
    }



}

