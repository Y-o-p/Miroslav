using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valsorim : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float test = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireArrow() {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
    }
}
