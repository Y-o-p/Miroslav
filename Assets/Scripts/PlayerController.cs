using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator animator;
    float speed = 5.0f;
    bool isGrounded;

    void Awake()
    {
        //get player rigidbody and animator
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        //player movement left and right
        playerRigidbody.velocity = new Vector2(horizontalInput * speed, playerRigidbody.velocity.y);

        //flip player sprite when moving left and right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector2.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        //player jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }

        animator.SetBool("isRunning", horizontalInput != 0);
        animator.SetBool("isGrounded", isGrounded);
    } 

    //player jump
    void Jump()
    {
        playerRigidbody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        isGrounded = false;
        animator.SetTrigger("jumpTrigger");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //check if player is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}   
    
    
    
    
    
    
    
    
    
    
//     Animator animator;
//     float jumpForce = 340.0f;
//     float runForce = 10.0f;
//     float maxRunSpeed = 2.0f;

//     private bool _isMoving = false;

//     public bool IsMoving {
//         get { 
//             return _isMoving; 
//         }
//         set { 
//             _isMoving = value;
//             animator.SetBool("IsMoving", _isMoving);
//         }
//     }
//     // Start is called before the first frame update
//     void Start()
//     {
//         playerRigidbody = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // Jump
//         if (Input.GetKeyDown(KeyCode.UpArrow) && this.playerRigidbody.velocity.y == 0)
//         {
//             this.playerRigidbody.AddForce(transform.up * this.jumpForce);
//         }

//         // move left and right
//         int key = 0;
//         if (Input.GetKey(KeyCode.RightArrow)) key = 1;
//         if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

//         // decide PC's speed
//         float speedx = Mathf.Abs(this.playerRigidbody.velocity.x);

//         // limit PC's speed and avoid acceleration.
//         if (speedx < this.maxRunSpeed)
//         {
//             this.playerRigidbody.AddForce(transform.right * key * this.runForce);
//         }

//         // reverse spring according to the direction
//         if (key != 0)
//         {
//             transform.localScale = new Vector3(key, 1, 1);
//         }

//         this.animator.speed = speedx/2.0f;

//         if (this.playerRigidbody.velocity.y == 0) {
//             this.animator.speed = speedx / 2.0f;
//         }
//         else {
//             this.animator.speed = 1.0f;
//         }
//     }
// }
