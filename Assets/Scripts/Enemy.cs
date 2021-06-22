using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;

    private GameObject player;

    private Rigidbody enemyRB;

    // Start is called before the first frame update
    [SerializeField]
    protected void Start()
    {
        enemyRB = this.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirectiion = (player.transform.position - this.transform.position).normalized;
        enemyRB.AddForce(lookDirectiion * speed);
    }
}
