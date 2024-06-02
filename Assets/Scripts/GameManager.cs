using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Player player;






    void FixedUpdate()
    {
        player.PlayerPlay();
    }

}
