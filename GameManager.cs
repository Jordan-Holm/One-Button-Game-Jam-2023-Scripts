using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameIsRunning = false;
    public GameObject aKeyIndicator;

    public Slider livesSlider;
    public Image fillColor;

    public PlayerController player;

    public int score;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckHearts();
    }

    IEnumerator RestartButton()
    {
        yield return new WaitForSeconds(3);
        
        aKeyIndicator.SetActive(true);
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    private void CheckHearts()
    {
        if (player.playerLives < 1) //Ends Game
        {
            if (player.playerIsAlive == false)
            {
                gameIsRunning = false;


                StartCoroutine(RestartButton());
            }
            livesSlider.value = 0;
            fillColor.GetComponent<Image>().color = Color.clear;
        }
        if (player.playerLives == 1)
        {
            livesSlider.value = .33f;
        }
        if (player.playerLives > 1)
        {
            livesSlider.value = .66f;
        }
        if (player.playerLives > 2)
        {
            livesSlider.value = 1;
        }
    }
}
