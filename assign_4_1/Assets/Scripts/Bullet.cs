using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * 10f;

        if(Mathf.Abs(transform.position.x) >= 8f || Mathf.Abs(transform.position.y) >= 5f) {
            Destroy(gameObject);
        }
    }
}
