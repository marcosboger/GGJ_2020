using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<TimeManager>().StopTime();
    }


    public void ResetLevel()
    {
        //FindObjectOfType<Player>().ResetPosition();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
