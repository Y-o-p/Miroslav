using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    SpriteRenderer sprite;
    Animator animator;
    public int health = 10;
    int alive = 1;
    int IFrame = 0;
    float speed = 5.0f; //player speed
    bool isGrounded; //check if player is grounded

    private float arrowCoolDown = 0.5f;

    public GameObject arrowPrefab;
    private GameObject arrow;
    public GameObject lose_panel;
    public Camera cam;

    public AudioSource jump_sound;
    public AudioSource coin_sound;
    public AudioSource save_sound;
    public AudioSource grunt_sound;
    public AudioSource death_sound;

    void Awake()
    {
        //get player rigidbody and animator
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        GameManager.respawnPoint = playerRigidbody.transform.position;
    }

    void Update()
    {
        if (!GameManager.game_over) {
            float horizontalInput = Input.GetAxis("Horizontal");
            if(arrowCoolDown >= 0)
            {
                arrowCoolDown -= Time.deltaTime;
            }

            //player movement left and right
            playerRigidbody.velocity = new Vector2(horizontalInput * speed, playerRigidbody.velocity.y);

            //flip player sprite when moving left and right
            if (horizontalInput > 0.01f)
            {
                sprite.flipX = false;
            }
            else if (horizontalInput < -0.01f)
            {
                sprite.flipX = true;
            }

            //player jump
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                Jump();
            }

            // Shoot an arrow if the spacebar is pressed 
            if ((Input.GetKeyDown(KeyCode.Space)))
            {
                if (arrowCoolDown <= 0)
                {
                    animator.SetTrigger("shootTrigger");
                    Invoke("SpawnArrow", 0.5f);
                    arrowCoolDown = 0.5f;
                }
            }

            animator.SetBool("isRunning", horizontalInput != 0); //check if player is running
            animator.SetBool("isGrounded", isGrounded); //check if player is grounded

            if (transform.position.y < -9.0f)
            {
                health = 0;
            }

            if (health <= 0) 
            {
                if (alive == 1)
                {
                    player_die();
                }
            }
        }
    } 

    //player jump
    void Jump()
    {
        playerRigidbody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        isGrounded = false;
        jump_sound.Play();
        animator.SetTrigger("jumpTrigger");
    }

    //spawn arrow
    void SpawnArrow()
    {
        //create arrow
        arrow = Instantiate<GameObject>(
            arrowPrefab,
            new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z), 
            Quaternion.identity
        );

        //set arrow direction
        Vector2 arrowDirection = !sprite.flipX ? Vector2.right : Vector2.left;
        arrow.GetComponent<PlayerArrow>().Initialize(arrowDirection);
    }

    public void Hurt(int damage) {
        if (IFrame == 0)
        {
            animator.SetTrigger("hurtTrigger");
            health -= damage;
            print(health);
            IFrame = 1;
            Invoke("IFramesEnd", 1f);
            grunt_sound.Play();
        }
    }

    void player_die()
    {
        GameManager.lives -= 1;
        death_sound.Play();
        animator.SetTrigger("deadTrigger");
        alive = 0;
        if (GameManager.lives == 0)
        {
            GameManager.game_over = true;
            GameManager.lives = 3;
        }
        else
        {
            lose_panel.SetActive(true);
        }
        //update_song();
    }
    public void respawn()
    {
        //heal player back to max hp.
        health = 10;
        //respawns player at checkpoint
        lose_panel.SetActive(false);
        playerRigidbody.velocity = new Vector2(0, 0);
        playerRigidbody.transform.position = GameManager.respawnPoint;
        GameManager.score = 0;
        animator.SetTrigger("respawnTrigger");
        alive = 1;
        cam.GetComponent<CameraFollow>().ResetPositionOnTarget();
    }

    public void IFramesEnd()
    {
        IFrame = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (alive == 1)
        {
            //check if player is grounded
            if (collision.gameObject.CompareTag("Ground"))
            {
                //the isGrounded flag is how we keep track of if the player is jumping or not.
                //it is based on whether they are in contact with a game object tagged as "Ground".
                //when you jump, we set isGrounded to false, and when you land, we set it to true.
                isGrounded = true;
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                this.Hurt(1);
            }
            else if (collision.gameObject.CompareTag("EProjectile"))
            {
                this.Hurt(2);
                Destroy(collision.gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Logic for coin collision
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            GameManager.score += 100;
            coin_sound.Play();
        }
        //when u reach a check point
        else if (collision.gameObject.CompareTag("CheckPoint"))
        {
            //Checks if flag has been touched. If touched, does not save or play sfx
            if (collision.gameObject.GetComponent<if_touched>().touched == false)
            {
                GameManager.respawnPoint = collision.transform.position;
                save_sound.Play();
                collision.gameObject.GetComponent<if_touched>().touched = true;
            }
        }
    }
}  