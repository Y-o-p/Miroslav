using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedEnemy : MonoBehaviour
{
    float health;
    float speed = 2f;
    Rigidbody2D rigid2D;
    Animator animator;
    float chanceToChangeDirections = 0.05f;
    float dead;
    float delta;
    float chancetoAttack = 0.01f;
    float attack;
    private GameObject projectile;
    public GameObject fireballPrefab;
    public AudioSource orc_death_sound;
    public string newTag = "Untagged";
    // Start is called before the first frame update
    void Start()
    {
        health = 25;
        dead = 0;
        attack = 0;
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == 0 && attack == 0)
        {
            Vector3 pos = transform.position;
            pos.x += speed/2 * Time.deltaTime;
            transform.position = pos;
            transform.localScale = new Vector3(speed, 2, 2);
        }
    }

    void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections && dead == 0)
        {
            speed *= -1;
        }

        if (Random.value < chancetoAttack && dead == 0 && attack == 0)
        {
            this.animator.SetTrigger("aTrigger");
            Invoke("spawnFireball", 1f);
            attack = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collidedWith = other.gameObject;
        if (other.tag == "PArrow" && dead == 0)
        {
            Destroy(collidedWith);
            health = health - 5;
            if (health <= 0)
            {
                orc_death_sound.Play(); 
                dead = 1;
                this.animator.SetTrigger("dTrigger");
                gameObject.tag = newTag;
            }
        }
    }
    void spawnFireball()
    {
        Invoke("allowAttack", 1f);
        projectile = Instantiate<GameObject>(
            fireballPrefab,
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.identity
        );

        //set arrow direction
        Vector2 projectileDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        projectile.GetComponent<enemyOrb>().Initialize(projectileDirection);
    }

    void allowAttack()
    {
        attack = 0;
    }
}
