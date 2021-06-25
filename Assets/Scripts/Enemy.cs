using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;

    private GameObject player;

    private Rigidbody enemyRB;


    void EnemiesMove()
    {
        Vector3 enemyDirection = (player.transform.position - this.transform.position).normalized;
        enemyRB.AddForce(enemyDirection * speed);
    }


    void OutOfBound()
    {
        if(this.transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

   
    protected void Start()
    {
        enemyRB = this.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        EnemiesMove();
        OutOfBound();
    }
}
