using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMNG : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyGunPrefab;

    float pro;

    void Start()
    {
        pro = 0.8f;

        StartCoroutine(CreateEnemy());
        StartCoroutine(DiscountPro());
    }

    IEnumerator CreateEnemy() {
        while(true) {
            yield return new WaitForSeconds(0.5f);
            
            for(int i=0; i<Random.Range(1, 5); i++) {
                if(Random.Range(0f, 1f) >= pro)
                    Instantiate(enemyGunPrefab, new Vector3(Random.Range(-8f, 8f), 5.4f, 0), Quaternion.identity, transform);
                else
                    Instantiate(enemyPrefab, new Vector3(Random.Range(-8f, 8f), 5.4f, 0), Quaternion.identity, transform);
            }
        }
    }

    IEnumerator DiscountPro() {
        while(pro > 0.2f) {
            yield return new WaitForSeconds(10f);
            pro -= 0.2f;
        }
    }
}
