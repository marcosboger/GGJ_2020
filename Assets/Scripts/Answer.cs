using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Enumerators;

public class Answer : MonoBehaviour
{
    /** The values this encounter will change */
    [SerializeField]
    private AnswerBlocks whatAnswer;

    private EncounterEffects effects;
    
    public EncounterEffects Effects {  get { return effects; } }
    public string ChosenAnswer {  get { return whatAnswer.ToString(); } }

    [SerializeField]
    private Event whatTriggers;

    /** choice text */
    private TextMeshProUGUI answerText;

    private void Awake()
    {
        answerText = GetComponent<TextMeshProUGUI>();
        answerText.canvasRenderer.SetAlpha(0.01f);
    }


    // Triggered on New Encounter event
    public void SetAnswerValues(GameObject go)
    {
        var encounter = go.GetComponent<EncounterManager>();
        if (!encounter)  { return; }
        effects = encounter.GetAnswerValues(whatAnswer);
        answerText.text = effects.answerText.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShowUp(1f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Ho scelto la risposta: " + whatAnswer.ToString());
            whatTriggers.Occurred(this.gameObject);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FadeDown(1f);
    }

    void ShowUp(float alpha)
    {
        answerText.canvasRenderer.SetAlpha(0.01f);
        answerText.CrossFadeAlpha(alpha, .75f, false);
    }

    void FadeDown(float alpha)
    {
        answerText.canvasRenderer.SetAlpha(alpha);
        answerText.CrossFadeAlpha(0f, .15f, false);
    }

    void ShowUp(Image image, float alpha)
    {
        image.canvasRenderer.SetAlpha(0.01f);
        image.CrossFadeAlpha(alpha, .75f, false);
    }

    void FadeDown(Image image, float alpha)
    {
        image.canvasRenderer.SetAlpha(alpha);
        image.CrossFadeAlpha(0f, .15f, false);
    }

}
