using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public float speed = 7;
    private int moveRight = 1;
    private Vector2 velocity;
    private Rigidbody2D mushroomBody;
    private BoxCollider2D collider;
    private bool hasCollide = false;
    private Vector3 scaler;

    // Start is called before the first frame update
    void Start()
    {
        scaler = transform.localScale / (float)5;
        collider = GetComponent<BoxCollider2D>();
        mushroomBody = GetComponent<Rigidbody2D>();
        mushroomBody.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
        ComputeVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCollide) moveMushroom();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * speed, 0);
    }

    void moveMushroom()
    {
        mushroomBody.MovePosition(mushroomBody.position + velocity * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            speed = 0;
            ComputeVelocity();
            collider.enabled = false; mushroomBody.velocity = Vector2.zero;
            mushroomBody.bodyType = RigidbodyType2D.Static;
            StartCoroutine("ScaleOut");
        }

        if (col.gameObject.CompareTag("Pipe"))
        {
            if (!hasCollide)
            {
                hasCollide = true;

                moveRight *= -1;
                ComputeVelocity();
            }
        }
    }

    void LateUpdate()
    {
        hasCollide = false;
    }

    // void OnBecameInvisible()
    // {
    //     Destroy(gameObject);
    // }

    IEnumerator ScaleOut()
    {

        // Vector2 direction = new Vector2(Random.Range(-1.0f, 1.0f), 1);
        // rigidBody.AddForce(direction.normalized * 10, ForceMode2D.Impulse);
        // rigidBody.AddTorque(10, ForceMode2D.Impulse);
        // // wait for next frame
        // yield return null;

        // render turning big for 3 frames
        for (int step = 0; step < 3; step++)
        {
            this.transform.localScale = this.transform.localScale + scaler;
            // wait for next frame
            yield return null;
        }


        for (int step = 0; step < 8; step++)
        {
            this.transform.localScale = this.transform.localScale - scaler;
            // wait for next frame
            yield return null;
        }


        // Destroy(gameObject);

    }
}
