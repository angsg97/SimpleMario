using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : Singleton<UIController>
{
    public CentralManager centralManager;
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

    public void showGameOverScreen()
    {
        foreach (Transform eachChild in transform)
        {
            eachChild.gameObject.SetActive(true);
        }

    }

    public void resetGame()
    {
        centralManager.resetGame();
        SceneManager.LoadScene("Lab4");

        foreach (Transform eachChild in transform)
        {
            if (eachChild.tag == "Game Over")
            {
                eachChild.gameObject.SetActive(false);
            }
        }
    }
}
