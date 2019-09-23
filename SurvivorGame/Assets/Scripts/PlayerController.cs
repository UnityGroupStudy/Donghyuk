using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;

    private float applySpeed;

    [SerializeField]
    private float jumpForce;

    private bool isCrouch = false;
    private bool isRun = false;
    private bool isGround = true;

    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    private CapsuleCollider capsuleCollider;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0f;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
    }


    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TryCrouch();
        Move(); 
        CharacterRotation();
        CameraRotation();
    }

    private void TryCrouch() {
        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            Crouch();
        }
    }

    private void Crouch() {
        isCrouch = !isCrouch;
        
        if(isCrouch) {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }

        StartCoroutine(CrouchCoroutine());
    }

    IEnumerator CrouchCoroutine() {
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;

        while(_posY != applyCrouchPosY) {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if(count > 15)
                break;
            yield return null;
        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0);
    }

    private void IsGround() {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }

    private void TryJump() {
         if(Input.GetKeyDown(KeyCode.Space) && isGround) {
             Jump();
         }
    }

    private void Jump() {
        if(isCrouch)
            Crouch();

        myRigid.velocity = Vector3.up * jumpForce;
    }

    private void TryRun() {
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            Running();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)) {
            RunningCancel();
        }
    }

    private void Running() {
        if(isCrouch)
            Crouch();

        isRun = true;
        applySpeed = runSpeed;
    }
    private void RunningCancel() {
        isRun = false;
        applySpeed = walkSpeed;
    }

    private void Move() {
         float _moveDirX = Input.GetAxisRaw("Horizontal");
         float _moveDirZ = Input.GetAxisRaw("Vertical");

         Vector3 _moveHorizontal = transform.right * _moveDirX;
         Vector3 _moveVertical = transform.forward * _moveDirZ;

         Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed ;

         myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CharacterRotation() {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0, _yRotation, 0) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void CameraRotation() {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
    }
}
