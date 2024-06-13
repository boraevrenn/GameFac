using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        {
            if (FindObjectsOfType<Player>().Length > 0)
            {
                player = FindObjectOfType<Player>();
                player.playerHealthCanvas = Instantiate(healthPrefab, (Vector2)player.transform.position + extendHealthBars, Quaternion.identity, transform);
                player.playerHealthCanvas.GetComponentInChildren<Slider>().value = player.health;
            }
        }
    }

    private void Update()
    {
        if (player == null)
        {
            if (FindObjectsOfType<Player>().Length > 0)
            {
                player = FindObjectOfType<Player>();
                player.playerHealthCanvas = Instantiate(healthPrefab, (Vector2)player.transform.position + extendHealthBars, Quaternion.identity, transform);
                player.playerHealthCanvas.GetComponentInChildren<Slider>().value = player.health;
            }

        }
        FollowObjects();
        UpdateHealthBars();
        IfNullDestroyHealthBars();
        AllEnemyMethods();
    }
    void FollowObjects()
    {
        if (player != null)
        {
            if (player.playerHealthCanvas != null)
                player.playerHealthCanvas.transform.position = (Vector2)player.transform.position + extendHealthBars;
        }

    }

    void IfNullDestroyHealthBars()
    {
        if (player != null)
        {
            if (player.health <= Mathf.Epsilon)
            {
                DestroyImmediate(player.playerHealthCanvas, true);
            }
        }
    }

    void UpdateHealthBars()
    {
        if (player != null)
            if(player.playerHealthCanvas != null)
            {
                player.playerHealthCanvas.gameObject.GetComponentInChildren<Slider>().value = player.health;
            }
    }

    void UpdateEnemies()
    {
        enemies = FindObjectsOfType<Enemy>().ToList();
    }

    void AddCanvasToEnemies()
    {
        if (enemies.Count > 0)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.enemyHealthCanvas == null)
                {
                    enemy.enemyHealthCanvas = Instantiate(healthPrefab, (Vector2)enemy.transform.position + extendHealthBars, Quaternion.identity, transform);
                    enemy.enemyHealthCanvas.GetComponentInChildren<Slider>().maxValue = enemy.health;
                }
            }
        }
    }

    void FollowEnemyPosition()
    {
        if (enemies.Count > 0)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.enemyHealthCanvas != null)
                {
                    enemy.enemyHealthCanvas.transform.position = (Vector2)enemy.transform.position + extendHealthBars;
                }
            }
        }
    }

    void UpdateEnemyHealth()
    {
        if (enemies.Count > 0)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.enemyHealthCanvas != null)
                {
                    enemy.enemyHealthCanvas.GetComponentInChildren<Slider>().value = enemy.health;
                }
            }
        }
    }

    void DestroEnemyHealts()
    {
        if (enemies.Count > 0)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.health <= Mathf.Epsilon)
                {
                    DestroyImmediate(enemy.enemyHealthCanvas);
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
        DestroEnemyHealts();
    }
}
