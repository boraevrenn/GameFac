using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Timer : MonoBehaviour
{
  [SerializeField] PathFinding pathFinding;

  [Header("Player In Minimum Distance Run Timer Values")]
  [SerializeField] float waitMinumumDistanceTimer;
  [SerializeField] float waitMinimumDistanceTotalTime;

  [Header("Patrol Values")]
  float a = 5;


  void Update()
  {
    waitForReturnEnemyToMinimumDistance();
  }

  void waitForReturnEnemyToMinimumDistance()
  {
    waitMinumumDistanceTimer -= Time.deltaTime;
    if (pathFinding.isPlayerInMinumumDistance && waitMinumumDistanceTimer <= Mathf.Epsilon)
    {
      waitMinumumDistanceTimer = waitMinimumDistanceTotalTime;
    }
  }

  public bool ReturnIsMinumumDistanceTimerSmallerZero()
  {
    return waitMinumumDistanceTimer <= Mathf.Epsilon;
  }

  public bool ReturnIsMinumumDistanceTimerGreaterZero()
  {
    return waitMinumumDistanceTimer > Mathf.Epsilon;
  }


}

