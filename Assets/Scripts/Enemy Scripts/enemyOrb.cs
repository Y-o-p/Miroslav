using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyOrb : MonoBehaviour
{
    
    public float speed = 2f;
    private Vector3 direction; 

    //initialize projectile direction
    public void Initialize(Vector2 initialDirection)
    {
        direction = initialDirection;
    }

    private void Update()
    {
        //move projectile
        transform.position += speed * Time.deltaTime * direction;
    }

    //destroy projectile when it leaves the screen
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
