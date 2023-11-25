using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    public float timeValue;
    [SerializeField] TMP_Text timerText;
    [SerializeField] int point = 0;

    private void Start()
    {
        timerText = gameObject.GetComponent<TMP_Text>();
    }

    private void Awake()
    {
        switch (point)
        {
            case 0:
                if (point < 10)
                {
                    timeValue = 30;
                }
                return;
            case 10:
                if (point < 20)
                {
                    timeValue = 25;
                }
                return;
        }
    }

    public void ResetTimer()
    {
        switch (point)
        {
            case 0:
                if (point < 10)
                {
                    timeValue = 30;
                }
                return;
            case 10:
                if (point < 20)
                {
                    timeValue = 25;
                }
                return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (timeValue  > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }
        DisplayTime(timeValue);
    }

    void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds+1);
    }

    public void StopTimer()
    {
        enabled = false;
        timerText.text = "0:00";
    }

}
