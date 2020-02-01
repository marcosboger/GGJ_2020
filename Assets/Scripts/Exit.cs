using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{

    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] float LevelExitSlowMoFactor = 0.2f;
    [SerializeField] AudioClip exitClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.gameObject.name == "Player_80")
            StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        AudioSource.PlayClipAtPoint(exitClip, Camera.main.transform.position);
        Time.timeScale = LevelExitSlowMoFactor;
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1f;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}