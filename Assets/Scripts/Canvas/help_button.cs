using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class help_button : MonoBehaviour
{
    public GameObject titleCanvas;
    public GameObject helpCanvas;

    public void OnClick()
    {
        
        titleCanvas.SetActive(false);
        helpCanvas.SetActive(true);
    }
}
