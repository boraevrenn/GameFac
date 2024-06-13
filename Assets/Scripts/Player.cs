using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioManager audioManager;
    public GameObject playerHealthCanvas;
    public GameObject playerInfoCanvas;
    [Header("Player Components")]
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Animator playerAnimator;
    [SerializeField] BoxCollider2D boxCollider;

    [Header("Adjustable Values")]
    public float moveSpeed = 500;
    public float jumpSpeed = 500;
    public float health;
    [SerializeField] float destroyTime;

    [Header("Attack Values")]
    [SerializeField] Vector2 extendAttackRadiusRight;
    [SerializeField] Vector2 extendAttackRadiusLeft;
    [SerializeField] float radius;
    public float attackDamage;
    [SerializeField] float attackTimer;
    [SerializeField] float attackTimerTotal;




    float rotationChangeValue = 1;


    private void Awake()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        if (audioManager == null)
            audioManager = FindObjectOfType<AudioManager>();
        if (playerRigidbody == null)
            playerRigidbody = GetComponent<Rigidbody2D>();
        if (playerAnimator == null)
            playerAnimator = GetComponent<Animator>();
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if (!gameManager.enterEditMode && !gameManager.gameOver)
        {
            PlayAnimations();
        }
        PlayerDeathAnimation();

    }

    void FixedUpdate()
    {
        if (!gameManager.enterEditMode && !gameManager.gameOver)
        {
            PlayerMovement();
        }
    }

    private void LateUpdate()
    {
        CameraFollow();
    }


    public void PlayerMovement()
    {
        Move();
        Jump();
        Rotate();
        PlayerAttack();

    }
    public void PlayAnimations()
    {
        MoveAnimation();
        JumpAnimation();
        AttackAnimation();

    }



    //Player move methods
    void Move()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(horizontalMovement * moveSpeed * Time.deltaTime, playerRigidbody.velocity.y);
        playerRigidbody.velocity = move;
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jump = new Vector2(playerRigidbody.velocity.x, jumpSpeed * Time.deltaTime);
            playerRigidbody.velocity = jump;
        }
    }

    void Rotate()
    {
        if (Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon)
        {
            if (playerRigidbody.velocity.x < -rotationChangeValue)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (playerRigidbody.velocity.x > rotationChangeValue)
                transform.localScale = new Vector3(1, 1, 1);
        }
    }


    //Player Animation Methods
    void MoveAnimation()
    {
        if (Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon)
            playerAnimator.SetBool("isRunning", true);
        else
            playerAnimator.SetBool("isRunning", false);
    }

    void JumpAnimation()
    {

        if (!playerRigidbody.IsTouchingLayers(LayerMask.GetMask("Ground")))
            playerAnimator.SetBool("isJump", true);
        else if (playerRigidbody.IsTouchingLayers(LayerMask.GetMask("Ground")))
            playerAnimator.SetBool("isJump", false);
    }


    void PlayerAttack()
    {
        attackTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && attackTimer <= Mathf.Epsilon)
        {
            attackTimer = attackTimerTotal;
            audioManager.PlaySwordSound();
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
                    if (LayerMask.LayerToName(damagedObject.gameObject.layer) == "Enemy")
                    {
                        currentAttackedObject = damagedObject.gameObject;
                    }
                }
            }

            if (currentAttackedObject != null)
            {

                Enemy enemy = currentAttackedObject.GetComponent<Enemy>();
                Animator enemyAnimator = enemy.GetComponent<Animator>();
                enemyAnimator.SetTrigger("Hurt");
                enemy.health -= attackDamage;
                if (enemy.health <= 0)
                {
                    Destroy(enemy.gameObject, destroyTime);

                }
            }
        }
    }


    void CameraFollow()
    {
        if (!gameManager.enterEditMode)
        {
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }

    }
    void AttackAnimation()
    {
        if (Input.GetMouseButton(0) && attackTimer <= Mathf.Epsilon)
        {
            playerAnimator.SetTrigger("Attack");
        }
    }


    void PlayerDeathAnimation()
    {
        if (health <= Mathf.Epsilon)
        {
            playerAnimator.SetTrigger("Dead");
        }
    }

}