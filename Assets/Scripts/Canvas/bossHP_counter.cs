using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossHP_counter : MonoBehaviour
{
    public Text s_text;
    GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Valsorim");
    }

    // Update is called once per frame
    void Update()
    {
        s_text.text = "Valsorim Hp: " + boss.GetComponent<Valsorim>().health;
    }
}
