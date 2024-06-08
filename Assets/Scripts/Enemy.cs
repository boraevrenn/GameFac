using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    public float health;
    [SerializeField] Vector2 extendAttackRadiusRight;
    [SerializeField] Vector2 extendAttackRadiusLeft;
    [SerializeField] float radius;
    [SerializeField] float attackTimer;
    [SerializeField] float attackTimerTotal;
    [SerializeField] float attackDamage;
    [SerializeField] PathFinding pathfinding;



    private void FixedUpdate()
    {
        EnemyAttack();
    }


    void EnemyAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= Mathf.Epsilon && (pathfinding.isPlayerInMinimumDistance || pathfinding.isPlayerInMaximumDistance))
        {
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
                    Destroy(player.gameObject);
                }
            }
        }
    }
    void AttackAnimation()
    {
        if (attackTimer <= Mathf.Epsilon)
        {
            enemyAnimator.SetTrigger("Attack");
        }
    }

}
