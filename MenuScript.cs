using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [Header("A button Params")]
    public Color startColor = Color.white;
    public Color clickedColor = Color.gray;

    [Header("Loading Bar")]
    public Transform loadingBarTransform;
    [SerializeField] private float currentAmount;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<SpriteRenderer>().color = clickedColor;
            transform.localScale = new Vector2(0.1f, 0.1f);

            loadingBarTransform.GetComponent<Image>().fillAmount = 0;
            
            if (currentAmount < 100)
            {
                currentAmount += speed * Time.deltaTime;
            } else
            {
                SceneManager.LoadScene("Game");
            }
            loadingBarTransform.GetComponent<Image>().fillAmount = currentAmount / 100;
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            gameObject.GetComponent<SpriteRenderer>().color = startColor;
            transform.localScale = new Vector2(0.15f, 0.15f);

            currentAmount = 0;
            loadingBarTransform.GetComponent<Image>().fillAmount = 0;
        }
    }

    
}
