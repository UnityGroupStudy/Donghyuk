﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

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
        //theCamera = FindObjectOfType<Camera>();
        myRigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Move(); 
        CharacterRotation();
        CameraRotation();
    }

    private void Move() {
         float _moveDirX = Input.GetAxisRaw("Horizontal");
         float _moveDirZ = Input.GetAxisRaw("Vertical");

         Vector3 _moveHorizontal = transform.right * _moveDirX;
         Vector3 _moveVertical = transform.forward * _moveDirZ;

         Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

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
