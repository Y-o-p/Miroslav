using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class help_button : MonoBehaviour
{
    public GameObject panel;

    public void OnClick()
    {
        
        panel.SetActive(true);
    }
}
