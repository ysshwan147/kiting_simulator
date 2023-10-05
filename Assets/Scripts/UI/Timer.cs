using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float sec;
    private float min;
    private Text timeText;
    private string time;

    // Start is called before the first frame update
    void Start()
    {
        timeText = transform.parent.GetComponent<Text>();
        GameManager.setTimerObject(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        sec += Time.deltaTime;
        if (sec >= 60f)
        {
            min += 1;
            sec = 0;
        }


        int minutes = Mathf.FloorToInt(min);
        int seconds = Mathf.FloorToInt(sec);
        time = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = time;

        // timeText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
    }

    public string getTime() {
        return time;
    }
}
