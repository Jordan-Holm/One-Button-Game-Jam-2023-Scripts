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

    [Header("Audio Clips")]
    public AudioClip attackClip;
    public AudioClip damagedSound;
    public AudioClip deathSound;
    public AudioClip healClip;
    public AudioClip startSound;

    private AudioSource playerAudioSource;
    public GameManager gameManager;
    private bool startgame = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startgame)
        {
            playerAudioSource.PlayOneShot(startSound);
        }
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

                playerAudioSource.PlayOneShot(attackClip);
            }
        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damageToRecieve)
    {
        if (damageToRecieve == -1)
        {
            playerLives -= 1;
            playerAnimator.Play("Hurt");
            if (playerLives > 1)
            {
                playerAudioSource.PlayOneShot(damagedSound);

            }

            if (playerLives <= 0)
            {
                playerAudioSource.PlayOneShot(deathSound);
                playerIsAlive = false;
            }
        } else
        {
            if (playerLives < 3 && playerIsAlive)
            {
                playerLives += 1;
                playerAudioSource.PlayOneShot(healClip);
            }
        }
    }

    public void PlayerDeath()
    {
        playerAnimator.Play("Death");
    }

    public void MovePlayerMenu()
    {
        playerAnimator.Play("Attack2");
        startgame = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
