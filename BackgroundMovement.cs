using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private float speed = 3f;
    private Vector2 startPos;

   public GameManager gameManager;

    private void Start()
    {
        startPos.x = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameIsRunning == false)
        {
            speed = 0;
        }

        if (transform.position.x <= -29)
        {
            transform.position = startPos;
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
