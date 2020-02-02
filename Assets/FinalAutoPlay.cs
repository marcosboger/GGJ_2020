using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAutoPlay : MonoBehaviour
{
    [SerializeField] private float waitToPlay = 1f;
    private GameObject TileManager;
    private GameObject TimeManager;
    private bool alreadyPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        TimeManager = GameObject.Find("Game Canvas");
        TileManager = GameObject.Find("Tile Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if(!alreadyPlaying)
        {
            alreadyPlaying = true;
            TimeManager.GetComponent<TimeManager>().StartTime();
            TileManager.GetComponent<TilemapBehaviour>().deactivateChanges();
        }
        
    }
}
