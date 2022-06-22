using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float maxSpeed = 10;
    public float upSpeed = 5;

    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool onGroundState = true;
    private bool faceRightState = true;
    private bool rightKeyPressed = false;
    private bool leftKeyPressed = false;
    private bool isDead = false;

    private Animator marioAnimator;
    private AudioSource marioAudio;

    // Scoring system vars
    // public Transform enemyLocation;
    // public Text scoreText;
    // public Text gameOverScoreText;
    // private int score = 0;
    // private bool countScoreState = false;

    // get game reset function from MenuController
    public UIController uiController;


    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();

        // Get sprite component
        marioSprite = GetComponent<SpriteRenderer>();

        // Get reference to mario's animator and audio
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();

        // subscribe to player event
        GameManager.OnPlayerDeath += PlayerDiesSequence;
    }

    void PlayerDiesSequence()
    {
        // Mario dies
        Debug.Log("Mario dies");
        // do whatever you want here, animate etc
        isDead = true;
        marioAnimator.SetTrigger("onDeath");
        // Give mario some vertical impulse
        marioBody.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
        int LayerIgnoreRaycast = LayerMask.NameToLayer("Player");
        gameObject.layer = LayerIgnoreRaycast;
        uiController.showGameOverScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            // toggle state
            if ((Input.GetKeyDown("a") && faceRightState && !rightKeyPressed) || (Input.GetKeyUp("d") && rightKeyPressed))
            {
                faceRightState = false;
                marioSprite.flipX = true;
                if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                    marioAnimator.SetTrigger("onSkid");
            }

            if ((Input.GetKeyDown("d") && !faceRightState && !leftKeyPressed) || (Input.GetKeyUp("a") && leftKeyPressed))
            {
                faceRightState = true;
                marioSprite.flipX = false;
                if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                    marioAnimator.SetTrigger("onSkid");
            }

            // when jumping, and Gomba is near Mario and we haven't registered our score
            // if (!onGroundState && countScoreState)
            // {
            //     if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            //     {
            //         countScoreState = false;
            //         score++;
            //         Debug.Log(score);
            //     }
            // }

            if (Input.GetKeyDown("z"))
            {
                CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z, this.gameObject);
            }

            if (Input.GetKeyDown("x"))
            {
                CentralManager.centralManagerInstance.consumePowerup(KeyCode.X, this.gameObject);
            }

            marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    void FixedUpdate()
    {
        // float moveHorizontal = Input.GetAxis("Horizontal");
        // Vector2 movement = new Vector2(moveHorizontal, 0);
        // marioBody.AddForce(movement * speed);
        // dynamic rigidbody

        if (!isDead)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                if (Mathf.Abs(marioBody.velocity.x) < maxSpeed)
                    marioBody.AddForce(movement * speed);
            }

            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                // stop
                marioBody.velocity = new Vector2(0, marioBody.velocity.y);
            }

            if (Input.GetKeyDown("space") && onGroundState)
            {
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
                // countScoreState = true; //check if Gomba is underneath
            }

            // Check for left and right movement key up and update state
            if (Input.GetKeyUp("a")) leftKeyPressed = false;
            if (Input.GetKeyUp("d")) rightKeyPressed = false;

            if (Input.GetKeyDown("a")) leftKeyPressed = true;
            if (Input.GetKeyDown("d")) rightKeyPressed = true;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacles") || col.gameObject.CompareTag("Pipe"))
        {
            onGroundState = true; // back on ground
            // countScoreState = false; // reset score state
            // scoreText.text = "Score: " + score.ToString();
        };
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         Debug.Log("Collided with Gomba!");
    //         gameOverScoreText.text = "Your Final Score: " + score.ToString();
    //         menuController.showGameOverScreen();
    //     }
    // }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }
}
