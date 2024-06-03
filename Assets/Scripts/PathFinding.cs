using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Player player;
    [SerializeField] Transform WalkBack;




    [Header("Enemy Values")]
    [SerializeField] float enemyMoveSpeed;



    [Header("Distance Values")]
    [SerializeField] float minimumDistance = 2;
    [SerializeField] float maximumDistance = 5;
    [SerializeField] float waitForReturn;
    [SerializeField] float waitTimeTotal;


    [Header("Random Walk")]
    [SerializeField] int randomNumberForWalkBackOrForward;
    [SerializeField] int minimumRandomValue;
    [SerializeField] int maximumRandomValue;
    [SerializeField] float generateRandomNumberTimer;
    [SerializeField] float generateRandomNumberTimerTotal;
    [SerializeField] int RandomTurnBackNumber;



    public void Pathfinding()
    {

        float distance = CalculateEnemyPlayerDistance();
        ReturnNumberForWalkBack(distance);
        ReturnTimer(distance);


        if (distance <= maximumDistance && distance >= minimumDistance
        && waitForReturn <= Mathf.Epsilon
        && randomNumberForWalkBackOrForward != 1)
        {
            EnemyMoveToward(player.transform.position, enemyMoveSpeed);
        }


        else if (distance <= minimumDistance || randomNumberForWalkBackOrForward == RandomTurnBackNumber)
        {
            EnemyMoveToward(WalkBack.transform.position, enemyMoveSpeed);
        }
    }

    void ReturnTimer(float distance)
    {
        waitForReturn -= Time.deltaTime;
        if (distance <= minimumDistance)
        {
            waitForReturn = waitTimeTotal;
        }
    }

    void ReturnNumberForWalkBack(float distance)
    {
        generateRandomNumberTimer -= Time.deltaTime;
        if (distance <= maximumDistance && generateRandomNumberTimer <= Mathf.Epsilon)
        {
            generateRandomNumberTimer = generateRandomNumberTimerTotal;
            randomNumberForWalkBackOrForward = UnityEngine.Random.Range(minimumRandomValue, maximumRandomValue);
        }
        else if (distance > maximumDistance)
        {
            randomNumberForWalkBackOrForward = 0;
        }
    }

    float CalculateEnemyPlayerDistance()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    void EnemyMoveToward(Vector2 positionObject, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, positionObject, speed * Time.deltaTime);
    }
}
