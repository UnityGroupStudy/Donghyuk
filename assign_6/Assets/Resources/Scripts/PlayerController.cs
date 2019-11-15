using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public static PlayerController I;

    CapsuleCollider coll;
    Rigidbody rigid;
    RaycastHit hitObject;
    Vector3 velocity;
    float rotationY;

    bool isGround = false;
    bool isMoveTile = false;
    Vector3 moveTileToPlayerPosition;
    float groundCheckDistance = 0.1f;

    float speed = 10;
    float walkSpeed = 10;
    float runSpeed = 20;

    float jumpForce = 200;
    bool isJump = false;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    float cameraRotationLimit;
    float currentCameraRotationX = 0f;

    [SerializeField]
    GameObject theCamera;
    float theCameraRotationX = 5;
    
    void Awake() {
        I = this;

        coll = GetComponent<CapsuleCollider>();
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        speed = walkSpeed;
    }

    void Update()
    {
        CheckKey();
        Rotate();

        //rigid.MovePosition(transform.position + velocity * Time.deltaTime);

        transform.position = transform.position + velocity * Time.deltaTime;
    }

    void FixedUpdate() {
        CheckIsGround();
        CheckJump();
    }

    void CheckIsGround() {
        isGround = Physics.Raycast(transform.position, Vector3.down, out hitObject , coll.bounds.extents.y + groundCheckDistance);
        if(isGround) {
            if(hitObject.transform.tag == "MoveTile") {
                transform.SetParent(hitObject.transform);
                /*if(isMoveTile == false) {
                    isMoveTile = true;
                    moveTileToPlayerPosition.x = hitObject.transform.position.x - transform.position.x;
                    moveTileToPlayerPosition.z = hitObject.transform.position.z - transform.position.z;
                    
                }
                if(isMoveTile == true) {
                    moveTileToPlayerPosition += (-velocity * Time.deltaTime);
                    Vector3 hitObjectPosition = hitObject.transform.position - moveTileToPlayerPosition;
                    transform.position = new Vector3(hitObjectPosition.x, transform.position.y, hitObjectPosition.z);
                }*/
            }
            else if(hitObject.transform.tag == "Ground") {
                transform.parent = null;
                isMoveTile = false;
            }
        }
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
        CameraRotation();
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

    private void CameraRotation() {
        if(Input.GetKey(KeyCode.UpArrow))
            theCameraRotationX -= 1f;
        if(Input.GetKey(KeyCode.DownArrow))
            theCameraRotationX += 1f;

        theCameraRotationX = Mathf.Clamp(theCameraRotationX, 2, 90);

        theCamera.transform.localEulerAngles = new Vector3(-theCameraRotationX, 0, 0);
    }
}
