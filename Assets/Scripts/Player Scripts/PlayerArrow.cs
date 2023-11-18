using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public float speed = 10f; //arrow speed
    private Vector3 direction; //arrow direction

    //initialize arrow direction
    public void Initialize(Vector2 initialDirection)
    {
        direction = initialDirection;
    }

    private void Update()
    {
        //move arrow
        transform.position += speed * Time.deltaTime * direction; 
    }

    //destroy arrow when it leaves the screen
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
