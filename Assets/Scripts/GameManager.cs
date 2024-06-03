using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Player player;
    [SerializeField] PathFinding pathFinding;



    void Update()
    {
        player.PlayAnimations();
        pathFinding.Pathfinding();
    }


    void FixedUpdate()
    {
        player.PlayerMovement();
    }

}
