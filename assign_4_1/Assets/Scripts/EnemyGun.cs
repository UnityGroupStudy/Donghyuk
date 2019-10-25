using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Start()
    {
        StartCoroutine(AutoShoot());
    }

    IEnumerator AutoShoot() {
        while(true) {
            yield return new WaitForSeconds(0.5f);

            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }
}
