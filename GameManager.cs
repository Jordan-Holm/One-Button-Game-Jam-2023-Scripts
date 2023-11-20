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

    private bool hasDoublePoints;
    private float doublePointsMaxTime = 5;
    public bool hasStopWatch;
    private float stopWatchMaxTime = 5;

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
        score += DoublePointsAdjustment(scoreToAdd);

        if (hasDoublePoints)
        {
            string doublePointText = score.ToString() + " X2";
            scoreText.text = doublePointText;
        }
        else
            scoreText.text = score.ToString();
    }

    public int DoublePointsAdjustment(int scoreToAdjust)
    {
        if (hasDoublePoints)
        {
            return scoreToAdjust * 2;
        } else
        {
            return scoreToAdjust;
        }
    }

    public IEnumerator StartDoublePoints()
    {
        Debug.Log("Has Double Points? " + hasDoublePoints);
        hasDoublePoints = true;
        yield return new WaitForSeconds(doublePointsMaxTime);
        hasDoublePoints = false;
        Debug.Log("Has Double Points? " + hasDoublePoints);
    }

    public IEnumerator StartStopWatch()
    {
        hasStopWatch = true;
        yield return new WaitForSeconds(stopWatchMaxTime);
        hasStopWatch = false;
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
