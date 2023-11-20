using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    public int playerLives = 3;
    public bool playerIsAlive = true;

    [Header("Animator")]
    public Animator playerAnimator;

    [Header("Attack Params")]
    [SerializeField] public float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask enemyLM;
    public float attackRange;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsAlive)
        {
            AttackMechanic();
        }

        if (!playerIsAlive)
        {
            PlayerDeath();
            gameManager.gameIsRunning = false;
        }
    }

    public void AttackMechanic()
    {
        if ( timeBtwAttack <= 0 )
        {
            if ( Input.GetKeyDown(KeyCode.A) )
            {
                playerAnimator.Play("Attack1");
                Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLM);
                for (int i = 0; i < enemyToDamage.Length; i++)
                {
                    enemyToDamage[i].GetComponent<EnemyController>().TakeDamage();
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void TakeDamage()
    {
        playerAnimator.Play("Hurt");
        playerLives -= 1;
        
        if (playerLives <= 0)
        {
            playerIsAlive = false;
        }
    }

    public void PlayerDeath()
    {
        playerAnimator.Play("Death");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}