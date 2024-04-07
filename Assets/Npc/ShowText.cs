using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    public string header;
    public string textValue;
    public TMP_Text textElement;

    private string combinedText;
    private string Completeheader;

    // Start is called before the first frame update
    void Start()
    {
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
}
