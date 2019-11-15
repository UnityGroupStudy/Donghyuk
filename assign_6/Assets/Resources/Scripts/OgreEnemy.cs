using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreEnemy : MonoBehaviour
{
    Vector3 playerPosition;
    public float speed = 3;
    public float followDistance = 9;

    bool isGroundChecked;
    float groundCheckDistance = 0.1f;
    BoxCollider coll;
    RaycastHit hitObject;

    public Animator animator;

    void Awake()
    {
        coll = GetComponent<BoxCollider>();
    }

    void Update()
    {
        transform.LookAt(PlayerController.I.transform);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        playerPosition = PlayerController.I.transform.position;

        float distance = Vector3.Distance(playerPosition, transform.position);

        if(distance <= followDistance && distance >= 1f) {
            FollowPlayer();
            animator.SetBool("Follow", true);
        }
        else {
            animator.SetBool("Follow", false);
        }

        CheckIsGround();
    }

    void FollowPlayer() {
        Vector3 _dirVector = playerPosition - transform.position;
        _dirVector.y = 0;
        _dirVector = _dirVector.normalized * speed * Time.deltaTime;

        transform.position = transform.position + _dirVector;
    }

    void CheckIsGround() {
        isGroundChecked = Physics.Raycast(transform.position, Vector3.down, 
            out hitObject , coll.bounds.extents.y + groundCheckDistance);

        if(isGroundChecked) {
            if(hitObject.transform.tag == "MoveTile") {
                transform.SetParent(hitObject.transform);
            }
            else if(hitObject.transform.tag == "Ground") {
                transform.parent = null;
            }
        }
    }
}
