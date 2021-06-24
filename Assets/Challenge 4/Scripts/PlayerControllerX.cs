using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    
    private float speed = 500;
    private float boost = 10.0f;
    private float normalStrength = 20; // how hard to hit enemy without powerup
    private float powerupStrength = 500000; // how hard to hit enemy with powerup
    public int powerUpDuration = 5;

    private bool hasBoost = true;
    public bool hasPowerup;

    private GameObject focalPoint;
    public GameObject powerupIndicator;
   
    public ParticleSystem spaceParticle;

    

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        SpaceTurbo();

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }
    
    
    void SpaceTurbo()
    {
        if (Input.GetKey(KeyCode.Space) && hasBoost)
        {
            playerRb.AddForce(focalPoint.transform.forward * boost, ForceMode.Impulse);
            spaceParticle.Play();
            hasBoost = false;
            StartCoroutine(BoostCooldown());
        }
    }


    IEnumerator BoostCooldown()
    {
        yield return new WaitForSeconds(2);
        spaceParticle.Stop();
        hasBoost = true;
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  other.gameObject.transform.position - this.transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }

        }
    }




}
