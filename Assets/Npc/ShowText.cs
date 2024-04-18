using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    public string header;
    public string textValue;
    public TMP_Text textElement;

    private string combinedText;
    private string Completeheader;


    // Fade Var
    private float fadeTime = 3.5f;
    private float currentAlpha = 1;
    private float requiredAlpha = 0;
    private float waitBeforeFade = 5f;




    // Start is called before the first frame update
    void Start()
    {
        textElement = FindObjectOfType<TMP_Text>();

        Completeheader = GenerateHeaderText(header);
        combinedText = Completeheader + textValue;
        textElement.text = combinedText;

    }

    // Update is called once per frame
    void Update()
    {
        Completeheader = GenerateHeaderText(header);
        combinedText = Completeheader +"\n" + textValue;
        textElement.text = combinedText;
    }
     private string GenerateHeaderText(string content)
    {
        return $"<size=120%><b>{content}</b></size>";
    }


    public IEnumerator fadeOutText()
    {
        Color color = textElement.color;
        yield return new WaitForSeconds(waitBeforeFade);

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
        {            
            color.a = Mathf.Lerp(currentAlpha, requiredAlpha, t);
            textElement.color = color;
            yield return null;
        }
        textElement.text = null;
        combinedText = null;
        textValue = null;
        header = null;
        color.a = 1;
        textElement.color = color;
    }

}
