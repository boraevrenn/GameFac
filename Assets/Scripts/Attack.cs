using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField] bool isPlayer;





    [Header("Attack Values")]
    [SerializeField] float attackTimer;
    [SerializeField] float radius;
    [SerializeField] Vector2 attackArea;




    float rotationValue = 180;




    public void SwordAttack()
    {
        if (Input.GetMouseButton(0))
        {
            List<Collider2D> colliderFromAttack = new List<Collider2D>();
            if (ReturnRotationLeftOrRight())
                colliderFromAttack = Physics2D.OverlapCircleAll(transform.position + (Vector3)attackArea, radius).ToList();
            else
                colliderFromAttack = Physics2D.OverlapCircleAll(transform.position + (-(Vector3)attackArea), radius).ToList();
            foreach (var item in colliderFromAttack)
            {
                if (LayerMask.LayerToName(item.transform.gameObject.layer) == "Enemy")
                {
                    Debug.Log(item.name);
                }
            }
        }
    }

    bool ReturnRotationLeftOrRight()
    {
        if (transform.rotation.y == rotationValue - rotationValue)
            return true;
        else
            return false;
    }


    void OnDrawGizmos()
    {
        if (transform.rotation.y == rotationValue - rotationValue)
            Gizmos.DrawWireSphere(transform.position + (Vector3)attackArea, radius);
        else
            Gizmos.DrawWireSphere(transform.position + (-(Vector3)attackArea), radius);
    }

}



