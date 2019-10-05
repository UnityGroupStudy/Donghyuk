using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public static KeyController I = null;
    public enum KeyType {
        none, down, up, ll, dd
    };

    float limitTime = 1.0f;

    public int keyLength;
    public float[] keyTimes;
    public float[] keyUpTimes;
    public int[] keyValues;

    public int[] keyDouble;
    public int[] keyDoubleUp;
    public int[] keyDoubleStay;

    public int[] keyIndexs;

    void Awake() {
        if(I == null)
            I = this;
    }

    void Start()
    {
        keyLength = (int)System.Enum.GetValues(typeof(KeyCode)).Length;

        keyTimes = new float[keyLength];
        keyUpTimes = new float[keyLength];
        keyValues = new int[keyLength];
        
        keyDouble = new int[keyLength];
        keyDoubleUp = new int[keyLength];
        keyDoubleStay = new int[keyLength];

        keyIndexs = new int[510];

        for(int i=0; i<keyLength; i++) {
            keyTimes[i] = 0;
            keyUpTimes[i] = 0;
            keyValues[i] = (int)System.Enum.GetValues(typeof(KeyCode)).GetValue(i);
            keyDouble[i] = 0;
            keyDoubleUp[i] = 0;
            keyDoubleStay[i] = 0;

            keyIndexs[keyValues[i]] = i;
        }
    }

    void LateUpdate()
    {
        for(int i=0; i<keyLength; i++) {
            if(Input.GetKey((KeyCode)keyValues[i])) {
                keyTimes[i] += Time.deltaTime;
                keyUpTimes[i] = 0;
            }
            else {
                keyTimes[i] = 0f;
                if(keyUpTimes[i] < limitTime)
                    keyUpTimes[i] += Time.deltaTime;
            }
        }
    }

    public int Index(KeyCode key) {
        return keyIndexs[(int)key];
    }
    public bool Stay(KeyCode key) {
        return Input.GetKey(key);
    }
    public bool Down(KeyCode key) {
        return Input.GetKeyDown(key);
    }
    public bool Up(KeyCode key) {
        return Input.GetKeyUp(key);
    }
    public bool Long(KeyCode key, float time) {
        return keyTimes[Index(key)] >= time;
    }
    public bool LongUp(KeyCode key, float time) {
        return Input.GetKeyUp(key) && (keyTimes[Index(key)] >= time);
    }
    public bool Double(KeyCode key, float time) {
        bool result = false;
        if(keyTimes[Index(key)] >= time || keyUpTimes[Index(key)] >= time)
            keyDouble[Index(key)] = 0;

        if(Input.GetKeyDown(key))
            keyDouble[Index(key)] += 1;

        if(keyDouble[Index(key)] >= 2) {
            result = true;
            keyDouble[Index(key)] = 0;
        }

        return result;
    }
    public bool DoubleUp(KeyCode key, float time) {
        bool result = false;
        if(keyTimes[Index(key)] >= time || keyUpTimes[Index(key)] >= time)
            keyDoubleUp[Index(key)] = 0;

        if(Input.GetKeyUp(key))
            keyDoubleUp[Index(key)] += 1;

        if(keyDoubleUp[Index(key)] >= 2) {
            result = true;
            keyDoubleUp[Index(key)] = 0;
        }

        return result;
    }
    public bool DoubleStay(KeyCode key, float time) {
        bool result = false;
        if(keyTimes[Index(key)] >= time || keyUpTimes[Index(key)] >= time)
            keyDoubleStay[Index(key)] = 0;
        
        if(Input.GetKeyUp(key) && keyDoubleStay[Index(key)] == 0)
            keyDoubleStay[Index(key)] += 1;

        if(keyDoubleUp[Index(key)] >= 2) {
            result = true;
            keyDoubleStay[Index(key)] = 0;
        }

        return result;
    }
}
