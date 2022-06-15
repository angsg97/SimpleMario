using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    private bool broken = false;
    public GameObject prefab;
    private AudioSource brickAudio;
    // Start is called before the first frame update
    void Start()
    {
        brickAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Bottom Edge");
        if (col.gameObject.CompareTag("Player") && !broken)
        {
            PlayBreakSound();
            broken = true;
            // assume we have 5 debris per box
            for (int x = 0; x < 5; x++)
            {
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void PlayBreakSound()
    {
        brickAudio.PlayOneShot(brickAudio.clip);
    }

}
