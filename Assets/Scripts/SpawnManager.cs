using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameConstants gameConstants;
    // Start is called before the first frame update
    void Start()
    {
        // subscribe to enemy death event
        GameManager.OnEnemyDeath += spawnEnemy;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        // spawn two goombaEnemy
        for (int j = 0; j < 2; j++)
            spawnFromPooler(ObjectType.goombaEnemy);
    }

    void spawnFromPooler(ObjectType i)
    {
        // static method access
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null)
        {
            //set position, and other necessary states
            item.transform.position = Random.Range(0, 2) == 0 ? gameConstants.goombaSpawnPointStart1 : gameConstants.goombaSpawnPointStart2;
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool.");
        }
    }

    public void spawnEnemy()
    {
        if (Random.Range(0, 2) == 0) spawnFromPooler(ObjectType.goombaEnemy);
        else spawnFromPooler(ObjectType.greenEnemy);
    }
}
