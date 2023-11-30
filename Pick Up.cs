using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("Pick Up Type")]
    public string type;
    public int livesToAdd;

    [Header("Pik Up Stats")]
    public float speed = 3f;

    public GameManager gameManager;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerController = GameObject.Find("HeroKnight").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameIsRunning == false)
        {
            speed = 0;
            
        }
        MovePickUp();
    }

    public void MovePickUp()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "HeroKnight" )
        {
            if(type == "Health" && playerController.playerLives < 3)
            {
                playerController.TakeDamage(1);
                playerController.PlayHealParticle();
                Destroy(gameObject);
            }

            if(type == "Double Points")
            {
                gameManager.StartCoroutine(gameManager.StartDoublePoints());
                Destroy(gameObject);
            }

            if(type == "Stop Watch")
            {
                gameManager.StartCoroutine(gameManager.StartStopWatch());
                Destroy(gameObject);
            }
        }
    }
}
