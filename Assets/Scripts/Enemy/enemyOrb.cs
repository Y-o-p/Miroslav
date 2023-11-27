using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyOrb : MonoBehaviour
{
    
    public float speed = 2f;
    private Vector3 direction;
    float delta;
    //initialize projectile direction
    public void Initialize(Vector2 initialDirection)
    {
        direction = initialDirection;
    }

    private void Update()
    {
        //move projectile
        transform.position += speed * Time.deltaTime * direction;
        this.delta += Time.deltaTime;
        if(this.delta >= 6)
        {
            Destroy(this.gameObject);
        }
    }
}
