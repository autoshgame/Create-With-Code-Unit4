using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float powerupStrength = 15.0f; 

    public bool hasPowerup = false;

    protected Rigidbody playerRB;

    [SerializeField] protected GameObject powerupIndicator;

    void playerMove()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRB.AddForce(Vector3.forward * speed * verticalInput, ForceMode.Impulse);
        float horizontalInput = Input.GetAxis("Horizontal");
        playerRB.AddForce(Vector3.right * speed * horizontalInput, ForceMode.Impulse);
        powerupIndicator.transform.position = transform.position + new Vector3(0, 0.5f, 0);
    }

    //destroy power up and set coroutine for power up
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("SpeedPowerUp"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountDownRoutine());
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemies") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 distanceFromEnemy = (collision.gameObject.transform.position - this.transform.position);
            enemyRigidbody.AddForce(distanceFromEnemy * powerupStrength, ForceMode.Impulse);
        }
    }

    //IEnumerator for power - up
    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        playerMove();
    }

}
