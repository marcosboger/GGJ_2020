using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{

    [TextArea(16, 20)] [SerializeField] string DialogueText;


    public string GetDialogue()
    {
        return DialogueText;
    }

}