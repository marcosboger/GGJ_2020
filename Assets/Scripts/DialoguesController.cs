using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguesController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText;
   [SerializeField] private Dialogue levelDialogue;


    // Start is called before the first frame update
    void Start()
    {
        LoadText();
    }

   

    public void LoadText()
    {
        this.uiText.text = levelDialogue.GetDialogue();
    }
}
