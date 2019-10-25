using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    GunMNG gunMNG;

    public float speed = 10f;
    Vector3 positionVector;
    Vector3 moveVector;

    void Start()
    {
        gunMNG = GetComponent<GunMNG>();

        positionVector = transform.position;
        moveVector = Vector2.zero;
    }

    void Update()
    {
        float _vertical = 0f;
        float _horizontal = 0f;

        if(Input.GetKey(KeyCode.LeftArrow))
            _horizontal = -1;
        if(Input.GetKey(KeyCode.RightArrow))
            _horizontal = 1;
        if(Input.GetKey(KeyCode.UpArrow))
            _vertical = 1;
        if(Input.GetKey(KeyCode.DownArrow))
            _vertical = -1;

        moveVector = (Vector3.right * _horizontal) + (Vector3.up * _vertical);
        moveVector = moveVector.normalized * Time.deltaTime * speed;
        
        positionVector += moveVector;

        if(Mathf.Abs(positionVector.x) >= 8f) {
            positionVector.x -= moveVector.x;
            positionVector.x = -positionVector.x;
        }
        if(Mathf.Abs(positionVector.y) >= 5f) {
            positionVector.y -= moveVector.y;
            positionVector.y = -positionVector.y;
        }

        transform.position = positionVector;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Enemy" || collider.tag == "EnemyBullet") {
            Destroy(collider.gameObject);
            if(gunMNG.RemoveLast() == false)
                Destroy(gameObject);
        }
        else if(collider.tag == "Item") {
            Destroy(collider.gameObject);
            gunMNG.AddGun();
        }
    }
}
