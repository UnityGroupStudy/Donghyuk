using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public enum KeyType {
        none, down, up, ll, dd
    };

    int keyLength;
    public float[] keyTimes;
    public int[] keyValues;

    void Start()
    {
        keyLength = (int)System.Enum.GetValues(typeof(KeyCode)).Length;

        keyTimes = new float[keyLength];
        keyValues = new int[keyLength];

        for(int i=0; i<keyLength; i++) {
            keyTimes[i] = 0;
            keyValues[i] = (int)System.Enum.GetValues(typeof(KeyCode)).GetValue(i);
        }
    }

    void Update()
    {
        for(int i=0; i<keyLength; i++) {
            if(Input.GetKey((KeyCode)keyValues[i]))
                keyTimes[i] += Time.deltaTime;
            if(Input.GetKeyUp((KeyCode)keyValues[i]))
                keyTimes[i] = 0f;
        }
    }
}
