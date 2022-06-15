using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;
    private int moveRight;
    private float originalX;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    private SpriteRenderer _renderer;
    private bool celebrate = false;
    private int currentDanceFrame = 0;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();

        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("Player Sprite is missing a renderer");
        }


        // Set initial position
        this.transform.position = Random.Range(0, 2) == 0 ? gameConstants.goombaSpawnPointStart1 : gameConstants.goombaSpawnPointStart2;

        // get the starting position
        originalX = transform.position.x;

        // randomise initial direction
        moveRight = Random.Range(0, 2) == 0 ? -1 : 1;
        if (moveRight == -1) _renderer.flipX = !_renderer.flipX;

        // compute initial velocity
        ComputeVelocity();

        // subscribe to player event
        GameManager.OnPlayerDeath += EnemyRejoice;
    }

    // animation when player is dead
    void EnemyRejoice()
    {
        Debug.Log("Enemy killed Mario");
        // do whatever you want here, animate etc
        moveRight = 0; // Stop moving
        celebrate = true;

    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * gameConstants.maxOffset / gameConstants.enemyPatroltime, 0);
    }

    void MoveEnemy()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (celebrate)
        {
            if (currentDanceFrame == gameConstants.framesToWaitForDance)
            {
                currentDanceFrame = 0;
                _renderer.flipX = !_renderer.flipX;
            }
            else
            {
                currentDanceFrame++;
            }

        }
        else if (Mathf.Abs(enemyBody.position.x - originalX) < gameConstants.maxOffset)
        {// move goomba
            MoveEnemy();
        }
        else
        {
            // change direction
            moveRight *= -1;
            _renderer.flipX = !_renderer.flipX;
            ComputeVelocity();
            MoveEnemy();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // check if it collides with Mario
        if (other.gameObject.tag == "Player")
        {
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            if (yoffset > 0.75f)
            {
                KillSelf();
            }
            else
            {
                // hurt player
                CentralManager.centralManagerInstance.damagePlayer();
            }
        }
    }

    void KillSelf()
    {
        // enemy dies
        CentralManager.centralManagerInstance.increaseScore();
        StartCoroutine(flatten());
        Debug.Log("Kill sequence ends");
    }

    IEnumerator flatten()
    {
        Debug.Log("Flatten starts");
        int steps = 5;
        float stepper = 1.0f / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

            // make sure enemy is still above ground
            this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
            yield return null;
        }
        Debug.Log("Flatten ends");
        this.gameObject.SetActive(false);
        this.transform.localScale = new Vector3(1, 1, 1);
        Debug.Log("Enemy returned to pool");
        yield break;
    }

    // IEnumerator celebrate()
    // {
    //     _renderer.flipX = !_renderer.flipX;
    //     yield return null;
    // }
}