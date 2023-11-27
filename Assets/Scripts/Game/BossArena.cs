using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArena : MonoBehaviour
{
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
            camera.GetComponent<CameraFollow>().zone = GetComponent<BoxCollider2D>();
            GameManager.player_in_arena = true;
        }
    }
}
