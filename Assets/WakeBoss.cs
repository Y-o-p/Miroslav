using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeBoss : MonoBehaviour
{
    public Valsorim boss;
    public Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            camera.GetComponent<CameraFollow>().target2 = boss.gameObject;
            boss.GetComponent<Animator>().SetTrigger("Phase1");
        }
    }
}
