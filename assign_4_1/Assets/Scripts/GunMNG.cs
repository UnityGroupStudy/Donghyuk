using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMNG : MonoBehaviour
{   
    List<Gun> gunList;
    public GameObject gunPrefab;

    void Start()
    {
        gunList = new List<Gun>();
        gunList.Add(transform.Find("Gun").GetComponent<Gun>());

        StartCoroutine(AutoShoot());
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
            AddGun();
    }

    public void AddGun() {
        Transform newGun = Instantiate(gunPrefab, transform.position, Quaternion.identity, transform).transform;
        newGun.localScale = Vector3.right * 0.5f + Vector3.up;
        
        if(gunList.Count % 2 == 0)
            newGun.localPosition = Vector3.up * 0.1f + (Vector3.left * 0.1f) * (gunList.Count/2);
        else if(gunList.Count % 2 == 1)
            newGun.localPosition = Vector3.up * 0.1f + (Vector3.right * 0.1f) * (gunList.Count/2 + 1);

        gunList.Add(newGun.GetComponent<Gun>());
    }

    public bool HasGun(Gun gun) {
        return !(gunList.IndexOf(gun) == -1);
    }

    public bool RemoveLast() {
        if(gunList.Count >= 2) {
            GameObject desGun = gunList[gunList.Count-1].gameObject;
            gunList.RemoveAt(gunList.Count-1);
            Destroy(desGun);
            return true;
        }
        return false;
    }

    IEnumerator AutoShoot() {
        while(true) {
            for(int i=0; i<gunList.Count; i++)
                gunList[i].Shoot();

            yield return new WaitForSeconds(0.3f);
        }
    }
}
