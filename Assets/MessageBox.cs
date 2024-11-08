using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageBox : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float typingSpeed = 0.05f;
    public string textToPrint;


    private void OnEnable()
    {
        //text.text = "";
        //StartCoroutine(printText());
    }

    public IEnumerator RestartCoroutine() 
    {
        StopCoroutine(printText());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(printText());
    }
    public void res() 
    {
        StartCoroutine(RestartCoroutine());
    }

    IEnumerator printText() 
    {
        text.text = "";
        yield return new WaitForSeconds(0.1f);
        foreach (char letter in textToPrint)
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}
