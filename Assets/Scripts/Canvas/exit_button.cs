using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit_button : MonoBehaviour
{
    public GameObject titleCanvas;
    public GameObject helpCanvas;

    public void OnClick()
    {

        titleCanvas.SetActive(true);
        helpCanvas.SetActive(false);
    }
}
