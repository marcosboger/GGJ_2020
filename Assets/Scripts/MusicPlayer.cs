using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> audioClips = new List<AudioClip>();


    private void Awake()
    {
        if(FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            this.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Play(int trackNumber)
    {
        //this.GetComponent<AudioSource>().PlayDelayed(audioClips[trackNumber]);   
    }

}
