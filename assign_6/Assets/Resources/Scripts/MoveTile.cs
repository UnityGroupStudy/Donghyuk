using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    int moveDirState;
    int[] xDir = {0, -1, 0, 1}, zDir = {1, 0, -1, 0};

    float speed = 2f;
    
    Vector3 moveVector;

    void Start()
    {
        moveVector = transform.localPosition;

        moveDirState = 0;
    }

    void Update()
    {
        moveVector += (
            (Vector3.forward * zDir[moveDirState]) +
             (Vector3.right * xDir[moveDirState])) * Time.deltaTime * speed;

        if(Mathf.Abs(moveVector.x) > 4.5f || Mathf.Abs(moveVector.z) > 4.5) {
            moveVector = transform.localPosition;
            moveDirState = (moveDirState + 1) % 4;
        }
        
        transform.localPosition = moveVector;
    }
}
