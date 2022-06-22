using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public List<GameObject> powerupIcons;
    private List<ConsumableInterface> powerups;
    public Texture redMushroomTexture;
    public Texture greenMushroomTexture;
    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<ConsumableInterface>();
        for (int i = 0; i < powerupIcons.Count; i++)
        {
            powerupIcons[i].SetActive(false);
            powerups.Add(null);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addPowerup(int index, ConsumableInterface i)
    {
        Debug.Log("adding powerup");
        if (index < powerupIcons.Count)
        {
            if (index == 0)
            {
                powerupIcons[index].GetComponent<RawImage>().texture = greenMushroomTexture;
            }
            else
            {
                powerupIcons[index].GetComponent<RawImage>().texture = redMushroomTexture;
            }
            powerupIcons[index].SetActive(true);
            powerups[index] = i;
        }
    }

    public void removePowerup(int index)
    {
        if (index < powerupIcons.Count)
        {
            powerupIcons[index].SetActive(false);
            powerups[index] = null;
        }
    }

    void cast(int i, GameObject player)
    {
        if (powerups[i] != null)
        {
            Debug.Log("Casted");
            if (i == 0)
            {
                player.GetComponent<PlayerController>().upSpeed += 10;
                StartCoroutine(removeSpeedEffect(player));
            }
            else
            {
                // give player jump boost
                player.GetComponent<PlayerController>().maxSpeed *= 2;
                StartCoroutine(removeJumpEffect(player));
            }
            // powerups[i].consumedBy(p); // interface method
            removePowerup(i);
        }
    }

    IEnumerator removeSpeedEffect(GameObject player)
    {
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<PlayerController>().upSpeed -= 10;
    }

    IEnumerator removeJumpEffect(GameObject player)
    {
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<PlayerController>().maxSpeed /= 2;
    }

    public void consumePowerup(KeyCode k, GameObject player)
    {
        switch (k)
        {
            case KeyCode.Z:
                cast(0, player);
                break;
            case KeyCode.X:
                cast(1, player);
                break;
            default:
                break;
        }
    }
}
