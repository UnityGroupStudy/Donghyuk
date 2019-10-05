using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    CapsuleCollider coll;
    Rigidbody rigid;
    Vector3 velocity;
    float rotationY;

    bool isGround = false;
    float groundCheckDistance = 0.1f;

    float speed = 10;
    float walkSpeed = 10;
    float runSpeed = 20;

    float jumpForce = 200;
    bool isJump = false;
    
    void Awake() {
        coll = GetComponent<CapsuleCollider>();
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        speed = walkSpeed;
    }

    void Update()
    {
        /*if(KeyController.I.Long(KeyCode.Space, 1f))
            Debug.Log("SPACE");
        if(KeyController.I.LongUp(KeyCode.Space, 1f))
            Debug.Log("SPACE UP");
        if(KeyController.I.Double(KeyCode.F, 0.3f))
            Debug.Log("F");
        if(KeyController.I.Double(KeyCode.G, 0.3f))
            Debug.Log("G");
        if(KeyController.I.DoubleUp(KeyCode.H, 0.3f))
            Debug.Log("H");*/

        CheckKey();
        Rotate();

        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    void FixedUpdate() {
        CheckIsGround();
        CheckJump();
    }

    void CheckIsGround() {
        isGround = Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + groundCheckDistance);
    }

    void CheckJump() {
        if(isJump) {
            rigid.AddForce(Vector3.up * jumpForce);
            isJump = false;
        }
    }
    
    void Rotate() {
        transform.Rotate(0, rotationY, 0);
    }

    void CheckKey() {
        CheckMoveKey();
        CheckRunKey();
        CheckRotateKey();
        CheckJumpKey();
    }

    void CheckMoveKey() {
        int _dash = 1;
        float _horizontal = 0f, _vertical = 0f;

        if(Input.GetKey(KeyCode.A))
            _horizontal = -1f;
        if(Input.GetKey(KeyCode.D))
            _horizontal = 1f;
        if(Input.GetKey(KeyCode.W))
            _vertical = 1;
        if(Input.GetKey(KeyCode.S))
            _vertical = -1;

        if(KeyController.I.Double(KeyCode.A, 0.5f))
            _dash = 50;
        if(KeyController.I.Double(KeyCode.D, 0.5f))
            _dash = 50;


        velocity = (transform.right * _horizontal) + (transform.forward * _vertical);
        velocity = velocity.normalized * speed * _dash;
    }

    void CheckRunKey() {
        if(Input.GetKey(KeyCode.LeftShift))
            speed = runSpeed;
        else
            speed = walkSpeed;
    }

    void CheckRotateKey() {
        rotationY = 0f;
        if(Input.GetKey(KeyCode.LeftArrow))
            rotationY = -1f;
        if(Input.GetKey(KeyCode.RightArrow))
            rotationY = 1f;
    }

    void CheckJumpKey() {
        if(Input.GetKeyDown(KeyCode.Space) && isGround && !isJump)
            isJump = true;
    }
}
