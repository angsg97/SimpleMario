using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// this has methods callable by players
public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    private GameManager gameManager;
    public static CentralManager centralManagerInstance;
    // add reference to PowerupManager
    public GameObject powerupManagerObject;
    private PowerUpManager powerUpManager;

    void Awake()
    {
        centralManagerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        powerUpManager = powerupManagerObject.GetComponent<PowerUpManager>();
    }

    public void increaseScore()
    {
        GameManager.Instance.increaseScore();
    }

    public void resetGame()
    {
        GameManager.Instance.resetScore();
        powerUpManager.removePowerup(0);
        powerUpManager.removePowerup(1);
    }

    public void damagePlayer()
    {
        GameManager.Instance.damagePlayer();
    }

    public void spawnEnemy()
    {
        GameManager.Instance.spawnEnemy();
    }
    public void consumePowerup(KeyCode k, GameObject g)
    {
        powerUpManager.consumePowerup(k, g);
    }

    public void addPowerup(int i, ConsumableInterface c)
    {
        powerUpManager.addPowerup(i, c);
    }

    public void changeScene(string sceneName)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        Debug.Log("Wow");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        GameManager.Instance.spawnEnemy();
        GameManager.Instance.spawnEnemy();
    }
}
