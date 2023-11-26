using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float force = 500.0f;
    public int damage = 1;
    Rigidbody2D body;
    public AudioSource arrow_sound;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.AddForce(transform.right * force);
        arrow_sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerController>().Hurt(damage);
        }
        Destroy(gameObject);
    }
}
