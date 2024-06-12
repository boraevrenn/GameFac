using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] GameObject healthPrefab;
    [SerializeField] Player player;
    [SerializeField] List<Enemy> enemies;
    [SerializeField] Vector2 extendHealthBars;

    private void Awake()
    {
        if (player == null)
            player = FindObjectOfType<Player>();
            player.playerHealthCanvas = Instantiate(healthPrefab, (Vector2)player.transform.position + extendHealthBars, Quaternion.identity, transform);



    }

    private void Update()
    {
        FollowObjects();
        UpdateHealthBars();
        IfNullDestroyHealthBars();
        AllEnemyMethods();
    }
    void FollowObjects()
    {
        if (player != null)
            player.playerHealthCanvas.transform.position = (Vector2)player.transform.position + extendHealthBars;
    }

    void IfNullDestroyHealthBars()
    {
        if (player == null)
        {
            DestroyImmediate(player.playerHealthCanvas, true);
        }
    }

    void UpdateHealthBars()
    {
        if (player != null)
            player.playerHealthCanvas.gameObject.GetComponentInChildren<Slider>().value = player.health;
    }

    void UpdateEnemies()
    {
        enemies = FindObjectsOfType<Enemy>().ToList();
    }

    void AddCanvasToEnemies()
    {
        if(enemies.Count > 0)
        {
            foreach(Enemy enemy in enemies)
            {
                if(enemy.enemyHealthCanvas == null)
                {
                    enemy.enemyHealthCanvas = Instantiate(healthPrefab, (Vector2)enemy.transform.position + extendHealthBars, Quaternion.identity, transform);
                    enemy.enemyHealthCanvas.GetComponentInChildren<Slider>().maxValue = enemy.health;
                }
            }
        }
    }

    void FollowEnemyPosition()
    {
        if(enemies.Count > 0)
        {
            foreach(Enemy enemy in enemies)
            {
                if(enemy.enemyHealthCanvas != null)
                {
                    enemy.enemyHealthCanvas.transform.position = (Vector2)enemy.transform.position + extendHealthBars;
                }
            }
        }
    }

    void UpdateEnemyHealth()
    {
        if(enemies.Count > 0)
        {
            foreach(Enemy enemy in enemies)
            {
                if(enemy.enemyHealthCanvas != null)
                {
                    enemy.enemyHealthCanvas.GetComponentInChildren<Slider>().value = enemy.health;
                }
            }
        }
    }

    void AllEnemyMethods()
    {
        UpdateEnemies();
        AddCanvasToEnemies();
        FollowEnemyPosition();
        UpdateEnemyHealth();
    }
}
