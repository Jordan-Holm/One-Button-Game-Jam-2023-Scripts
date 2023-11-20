using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Stats")]
    public bool isAlive = true;
    public float speed;

    [Header("Animator")]
    public Animator enemyAnimator;

    public GameManager gameManager;

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
            speed = 0;
            enemyAnimator.SetBool("GameIsRunning", false);
        }
        
        MoveEnemy();

    }

    private void MoveEnemy()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isAlive)
        {
            enemyAnimator.Play("Attack");
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }


    public void TakeDamage()
    {
        enemyAnimator.Play("LightBandit_Hurt");
        
        isAlive = false;
        speed = 3f;
    }
}
