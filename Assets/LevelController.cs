using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<TimeManager>().StopTime();
    }


    void ResetLevel()
    {
        //FindObjectOfType<Player>().ResetPosition();
    }
}
