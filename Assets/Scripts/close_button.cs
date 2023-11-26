using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close_button : MonoBehaviour
{

    public GameObject panel;

    public void OnClicked()
    {

        panel.SetActive(false);
    }
}
