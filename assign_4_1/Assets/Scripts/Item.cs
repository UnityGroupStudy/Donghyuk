using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * 2f;

        if(transform.position.y <= -6f)
            Destroy(gameObject);
    }
}
