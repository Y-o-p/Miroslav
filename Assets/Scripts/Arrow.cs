using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float force = 500.0f;
    public int damage = 1;
    public bool enemyArrow = false;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.AddForce(transform.right * force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        GameObject obj = other.gameObject;
        if ((obj.tag == "Enemy" && !enemyArrow) || (obj.tag == "Player" && enemyArrow)) {
            print("Damaged entity for " + damage);
        }
        Destroy(gameObject);
    }
}
