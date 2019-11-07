using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject player;
    Rigidbody rBody;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        transform.LookAt(player.transform);
        
        rBody.velocity = Vector3.zero;
        if(Vector3.Distance(transform.position, player.transform.position) <= 25f) {
            rBody.AddRelativeForce(Vector3.forward * 500f);
        }
    }
}
