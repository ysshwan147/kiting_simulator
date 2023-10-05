using UnityEngine;
using UnityEngine.UI;

public class ElapsedTime : MonoBehaviour
{
    private Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeText = transform.parent.GetComponent<Text>();

        string elapsedTime = PlayerPrefs.GetString("LastTime", "Time");
        timeText.text = elapsedTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
