using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject camera;
    public float parallaxEffect;
    float offset = 18;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = camera.transform.position.x * parallaxEffect / 9.0f + offset * (int)(camera.transform.position.x / offset);
        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }
}
