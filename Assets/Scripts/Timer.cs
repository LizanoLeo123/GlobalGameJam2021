using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timer;

    float startTime = 0;
    public float seconds = 0;
    public float minutes = 0;


    public bool gameEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("DuringGame");
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnded)
        {
            startTime += Time.deltaTime;

            minutes = ((int)startTime / 60);
            seconds = (int)(startTime % 60);
        }
        timer.text = "TIME: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
