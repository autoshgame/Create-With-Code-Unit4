using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float powerupStrength = 15.0f; 

    public bool hasPowerup = false;

    protected Rigidbody playerRB;

    protected GameObject focalPoint;
    [SerializeField] protected GameObject powerupIndicator;

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountDownRoutine());
        }
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 distanceFromEnemy = (collision.gameObject.transform.position - this.transform.position);
            enemyRigidbody.AddForce(distanceFromEnemy * powerupStrength, ForceMode.Impulse);
        }
    }


    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    
    void Update()
    {
        float fowardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * speed * fowardInput, ForceMode.Impulse);
        powerupIndicator.transform.position = transform.position + new Vector3(0, 0.5f, 0);
    }

}
