using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class life_counter : MonoBehaviour
{
    public Text s_text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        s_text.text = "Lives " + GameManager.lives;
    }
}
