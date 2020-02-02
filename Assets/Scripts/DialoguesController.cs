using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguesController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
   [SerializeField] private Dialogue levelDialogue;



    public void AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(uiText != null )
        {
            timer -= Time.deltaTime;
            if(timer <= 0f )
            {
                timer += timePerCharacter;
                characterIndex++;
                uiText.text = textToWrite.Substring(0, characterIndex);


                if(characterIndex >= textToWrite.Length)
                {
                    uiText = null;
                    return;
                }
            }
        }
    }
}
