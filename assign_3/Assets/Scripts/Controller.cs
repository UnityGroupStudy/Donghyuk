using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody rBody;
    float speed = 4f;
    
    float jumpForce = 200f;
    int jumpCount;

    void Awake() {
        rBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        float _vertical = Input.GetAxis("Vertical");
        float _horizontal = Input.GetAxis("Horizontal");

        rBody.MovePosition(transform.position + (Vector3.forward * _vertical + Vector3.right * _horizontal) * Time.deltaTime * speed);

        if(Input.GetKeyDown(KeyCode.Space) && jumpCount < 3) {
            rBody.velocity = Vector3.zero;
            rBody.AddForce(Vector3.up * jumpForce);
            jumpCount += 1;
        }
    }

    void OnCollisionEnter(Collision collision) {
        jumpCount = 0;
    }
}
