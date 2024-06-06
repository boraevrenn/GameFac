using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Timer : MonoBehaviour
{
  [SerializeField] PathFinding pathFinding;

  [SerializeField] float waitMinumumDistanceTimer;
  [SerializeField] float waitMinimumDistanceTotalTime;


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


}

