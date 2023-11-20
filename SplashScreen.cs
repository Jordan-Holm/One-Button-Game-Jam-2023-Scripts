using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public static int SceneNumber;

    public GameObject logoScreen;

    private void Start()
    {
        //Fade in effect
        logoScreen.GetComponent<Animation>().Play("FadeIn");

        //Starts time to main menu
        if (SceneNumber == 0)
        {
            StartCoroutine(ToMainMenu());
        }
    }

    IEnumerator ToMainMenu() //Timer to load menu scene
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
    }
}