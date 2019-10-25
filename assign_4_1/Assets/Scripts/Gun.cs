using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;

    public void Shoot() {
        Instantiate(bulletPrefab, transform.position + Vector3.up, Quaternion.identity);
    }
}
