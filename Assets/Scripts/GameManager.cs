using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Player player;




    void Update()
    {

    }

    void FixedUpdate()
    {
        player.Move();
        player.Rotate();
        player.Jump();
    }

}
