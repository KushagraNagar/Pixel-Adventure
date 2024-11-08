using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    public string messsageText;
    public GameObject messageCanvas;
    public MessageBox messageBox;

    private void  OnTriggerEnter2D(Collider2D collision)
    {
        messageCanvas.SetActive(true);
        messageBox.textToPrint = messsageText;
        messageBox.res();
        Destroy(this.gameObject);
    }

    
}
