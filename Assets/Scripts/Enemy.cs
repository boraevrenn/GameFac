using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator enemyAnimator;
    [SerializeField] PathFinding pathfinding;
    [SerializeField] GameManager gameManager;
    [SerializeField] Patrol patrol;
    public GameObject enemyHealthCanvas;


    [Header("Attack Values")]
    [SerializeField] Vector2 extendAttackRadiusRight;
    [SerializeField] Vector2 extendAttackRadiusLeft;
    [SerializeField] float radius;
    [SerializeField] float attackTimer;
    [SerializeField] float attackTimerTotal;
    [SerializeField] float attackDamage;
    [SerializeField] float destroyTime;
    public float health;


    private void Awake()
    {
        if(enemyAnimator == null)
            enemyAnimator = GetComponent<Animator>();
        if(pathfinding == null)
            pathfinding = GetComponent<PathFinding>();
        if(gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        if(patrol == null)
            patrol = GetComponent<Patrol>();
    }
    private void Update()
    {
        if (!gameManager.enterEditMode && !gameManager.gameOver)
        {
            WalkAnimation();
            EnemyDeathAnimation();
        }
    }

    private void FixedUpdate()
    {
        if (!gameManager.enterEditMode && !gameManager.gameOver)
        {
            EnemyAttack();

        }

    }


    void EnemyAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= Mathf.Epsilon && (pathfinding.isPlayerInMinimumDistance || pathfinding.isPlayerInMaximumDistance))
        {
            enemyAnimator.SetTrigger("Attack");
            attackTimer = attackTimerTotal;
            List<Collider2D> attackObjectList = new List<Collider2D>();
            GameObject currentAttackedObject = null;
            if (transform.localScale.x == 1)
            {
                attackObjectList = Physics2D.OverlapCircleAll((Vector2)transform.position + extendAttackRadiusRight, radius).ToList();
            }
            else if (transform.localScale.x == -1)
            {
                attackObjectList = Physics2D.OverlapCircleAll((Vector2)transform.position + extendAttackRadiusLeft, radius).ToList();
            }


            if (attackObjectList.Count > 0)
            {
                foreach (Collider2D damagedObject in attackObjectList)
                {
                    if (LayerMask.LayerToName(damagedObject.gameObject.layer) == "Player")
                    {
                        currentAttackedObject = damagedObject.gameObject;
                    }
                }
            }

            if (currentAttackedObject != null)
            {
                Player player = currentAttackedObject.GetComponent<Player>();
                player.health -= attackDamage;
                if (player.health <= 0)
                {
                    Destroy(player.gameObject, destroyTime);
                }
            }
        }
    }

    void EnemyDeathAnimation()
    {
        if (health <= Mathf.Epsilon)
        {
            enemyAnimator.SetTrigger("Dead");
        }
    }

    void WalkAnimation()
    {
        if (patrol.isPatrolModeEnabled || pathfinding.isPlayerInMaximumDistance)
        {
            enemyAnimator.SetBool("isWalk", true);
        }
        else if (pathfinding.isPlayerInMinimumDistance)
        {
            enemyAnimator.SetBool("isWalk", false);
        }
        else
        {
            enemyAnimator.SetBool("isWalk", false);
        }
    }


}
