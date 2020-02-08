using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;

    private float speed = 5;
    public bool hasPowerup = false;
    private float powerupStrength = 15;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        
        //Game over after player falls
        if(transform.position.y < -10)
        {
            gameOver = true;
            Debug.Log("GAME OVER!");
        }

        //If camera rotation using Q & E
        //float sideInput = Input.GetAxis("Horizontal");
        //playerRb.AddForce(focalPoint.transform.right * sideInput * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            //Not to collect 2 powerups at same time
            if (!hasPowerup)
            {
                //Destroy powerup after consumption
                hasPowerup = true;
                Destroy(other.gameObject);

                //Powerup cooldown timer
                StartCoroutine(PowerUpCooldown());

                //Indicator
                powerupIndicator.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator PowerUpCooldown()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            
            Debug.Log("Player has collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }
}
