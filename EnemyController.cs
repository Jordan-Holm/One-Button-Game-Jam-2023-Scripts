using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Stats")]
    public bool isAlive = true;
    public float speedMax;
    public float speedMin = 3.5f;
    public float defaultSpeed = 3f;
    public int scoreWorth = 10;

    [Header("Animator")]
    public Animator enemyAnimator;

    public GameManager gameManager;
    public AudioClip damageSound;
    public AudioClip attackSound;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameIsRunning == false)
        {
            enemyAnimator.SetBool("GameIsRunning", false);
        }
        
        MoveEnemy();

    }

    private void MoveEnemy()
    {
        if (gameManager.hasStopWatch & isAlive)
            transform.Translate(Vector2.left * (RandomSpeed(speedMin, speedMax) * 0.5f) * Time.deltaTime);
        else
            transform.Translate(Vector2.left * RandomSpeed(speedMin, speedMax) * Time.deltaTime);

        if (transform.position.x <= -10)
        {
            Destroy(gameObject);
        }
    }

    private float RandomSpeed(float speedMin, float speedMax)
    {
        if (gameManager.gameIsRunning == false)
            defaultSpeed = 0;
        else if (!isAlive)
            defaultSpeed = 3;
        else
            defaultSpeed = Random.Range(speedMin, speedMax);
        Debug.Log(defaultSpeed);
        return defaultSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isAlive)
        {
            enemyAnimator.Play("Attack");
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(-1);
            gameObject.GetComponent<AudioSource>().PlayOneShot(attackSound);
        }
    }

    public void TakeDamage()
    {
        enemyAnimator.Play("LightBandit_Hurt");
        gameObject.GetComponent<AudioSource>().PlayOneShot(damageSound);
        
        isAlive = false;


        gameManager.AddScore(scoreWorth);
    }
}
