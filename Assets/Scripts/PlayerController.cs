using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected float speed;

    protected Rigidbody playerRB;

    protected GameObject focalPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float fowardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * speed * fowardInput, ForceMode.Impulse);
    }

}
