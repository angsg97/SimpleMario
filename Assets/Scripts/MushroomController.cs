using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public float speed = 7;
    private int moveRight = 1;
    private Vector2 velocity;
    private Rigidbody2D mushroomBody;
    private bool hasCollide = false;

    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        mushroomBody.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
        ComputeVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        moveMushroom();
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

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
