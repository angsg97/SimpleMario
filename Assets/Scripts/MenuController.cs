using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.tag == "Game Over")
            {
                eachChild.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score")
            {
                // disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    public void showGameOverScreen()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.tag == "Main Menu")
            {
                eachChild.gameObject.SetActive(false);
            }
            else
            {
                eachChild.gameObject.SetActive(true);
            }
        }

        Time.timeScale = 0.0f;
    }

    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
