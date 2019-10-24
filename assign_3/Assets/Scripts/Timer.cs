using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TextMesh timeText;
    int timeCount = 90;


    void Start()
    {
        timeText = GetComponent<TextMesh>();
        timeText.text = (timeCount/60).ToString() + " : " + (timeCount%60).ToString();

        StartCoroutine(Count());
    }

    IEnumerator Count() {
        while(timeCount > 0) {
            yield return new WaitForSeconds(1f);

            timeCount -= 1;

            timeText.text = (timeCount/60).ToString() + " : " + (timeCount%60).ToString();
        }
    }
}
