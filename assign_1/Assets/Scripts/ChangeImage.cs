using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeImage : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material[] materials = new Material[5];

    public Transform[] models = new Transform[2];

    void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        for(int i=0; i<5; i++) {
            materials[i] = (Material)Resources.Load("Images/Materials/" + (i+1).ToString());
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(AutoChange());
    }

    IEnumerator AutoChange() {
        while(true) {
            yield return new WaitForSeconds(0.5f);

            meshRenderer.material = materials[Random.Range(0, 5)];

            for(int i=0; i<2; i++) {
                models[i].position = new Vector3(Random.Range(-2, 2), Random.Range(2, 4), Random.Range(-2, 2));
                models[i].rotation = Quaternion.Euler(-90, Random.Range(0, 360), 0);
            
            }
        }
    }
}
