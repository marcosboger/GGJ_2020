using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{

    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] float LevelExitSlowMoFactor = 0.2f;
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    [SerializeField] AudioClip exitClip;
    private GameObject _player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Player>())
            StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        _player.GetComponent<Player>().fadeOut();
        AudioSource.PlayClipAtPoint(exitClip, Camera.main.transform.position);
        //Time.timeScale = LevelExitSlowMoFactor;
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1f;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        FindObjectOfType<DialoguesController>().LoadText();
    }


    Material myMaterial;
    Vector2 offSet;

    // Use this for initialization
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, backgroundScrollSpeed);
        _player = GameObject.Find("Player_80");
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }

}