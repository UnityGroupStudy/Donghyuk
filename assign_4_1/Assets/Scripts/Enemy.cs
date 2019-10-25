using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject itemPrefab;

    Vector3 moveVector;
    float sinCount;

    void Start()
    {
        moveVector = Vector3.zero;
        sinCount = 0;
    }

    void Update()
    {
        moveVector.x = Mathf.Sin(sinCount);
        sinCount += 0.1f;
        if(sinCount >= 10000f)
            sinCount = 0;

        transform.position += moveVector * Time.deltaTime * 5f;
        transform.position += Vector3.down * Time.deltaTime * 2f;

        if(transform.position.y <= -6f)
            Destroy(gameObject);
    }

    public void Die() {
        if(Random.Range(0f, 1f) >= 0.9f)
            Instantiate(itemPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Bullet") {
            Destroy(collider.gameObject);
            Die();
        }
    }
}
