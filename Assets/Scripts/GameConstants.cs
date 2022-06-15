using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    // for Scoring system
    int currentScore;
    int currentPlayerHealth;

    // for Reset values
    public Vector3 goombaSpawnPointStart1 = new Vector3(-10f, -0.45f, 0); // hardcoded location
    public Vector3 goombaSpawnPointStart2 = new Vector3(-7f, -0.45f, 0); // hardcoded location
    // .. other reset values  

    // for Consume.cs
    public int consumeTimeStep = 10;
    public int consumeLargestScale = 4;

    // for Break.cs
    public int breakTimeStep = 30;
    public int breakDebrisTorque = 10;
    public int breakDebrisForce = 10;

    // for SpawnDebris.cs
    public int spawnNumberOfDebris = 10;

    // for Rotator.cs
    public int rotatorRotateSpeed = 6;

    // for testing
    public int testValue;

    // for EnemyController.cs
    public int maxOffset = 6;
    public int enemyPatroltime = 3;
    public int groundSurface = -1;
    public int framesToWaitForDance = 4;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
