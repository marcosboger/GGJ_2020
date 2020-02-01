using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    float defaultTimeScale;

    private void Awake()
    {
        defaultTimeScale = 1;
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void StartTime()
    {
        Time.timeScale = defaultTimeScale;
    }
}
