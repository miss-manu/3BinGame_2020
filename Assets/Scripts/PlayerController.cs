using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==========================================
// The Main Player Script
//==========================================

public class PlayerController : MonoBehaviour
{
    public List<string> greenBinInventory;          // Stores items collected 
    
    // Player properties to move left and right
    public float speed = 3f;                        // Maximum speed for robot
    private float move = 0f;                        // Value to hold if our robot is moving
    
    // Player properties to jump and land
    public Transform groundPos;                     // Part of the player that would be grounded
    public float jumpForce;                         // Jump force for player
    private bool isGrounded = false;                // To hold the value returned from 2D overlapcircle
    public float checkRadius;                       // Radius around the check point, to ensure it is touching the ground
    public LayerMask whatIsGround;                  // To check if the player is on the 'ground'
    private bool isJumping = false;                  // To check for jump input

    // Player properties to jump longer, while pressing button
    //private bool longJump = false;                   // Check how long the player jumps
    //public float jumpTime;                          // To determine the max time a player can jump
    //private float jumpTimeCounter;                  // Countdown how long the player has pressed jump

    // Component references
    private Rigidbody2D rb2d;                       // Value to represent our rigidbody
    private Animator anim;                          // Value to represent our Animator

    // UI references
    public Text scoreText;                          // 

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();         // Set reference component
        anim = GetComponent<Animator>();            // Set ref Animator component
        greenBinInventory = new List<string>();     // Set list component
    }


    private void Update()
    {
        //==========================================
        // INPUT SCRIPTS
        //==========================================

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;                       // Moved user input here (from fixed update) to avoid loss of input on key presses
        }

        //  Attempting to allow the player to hold jump key and jump higher
        //  Using this tutorial: https://www.youtube.com/watch?v=j111eKN8sJw
        /*if (Input.GetButton("Jump") && isJumping)
        {
            longJump = true;
        }

            if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }*/
    }

    private void FixedUpdate()
    {
        //==========================================
        // MOVEMENT SCRIPTS
        //==========================================

        // Check first to see if the player is grounded, as can only jump from ground layer
        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);

        // If so, jump player
        if (isGrounded && isJumping)
        {
            AudioManager.PlaySound("Bounce");         // Play sound when the player jumps

            rb2d.velocity = Vector2.up * jumpForce;
            //rb2d.AddForce(new Vector2(rb2d.velocity.x, jumpForce)); 


            /*if (longJump)
            {
                jumpTimeCounter = jumpTime;

                if (jumpTimeCounter > 0)
                {
                    rb2d.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    longJump = false;
                }
            }*/

            isJumping = false;                      // Restore until jump is requested (through update function)
        }

        // Get input value of horizontal axes to find if player is moving left (-1) or right (1)
        move = Input.GetAxis("Horizontal");     
        
        if (move < 0)           // If player is moving left
        {
            rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y); // Move the player on x axis, keep y axis at same value
            transform.eulerAngles = new Vector3(0, 180, 0);             // Euler angles to change the sprites rotation position
            //transform.localScale = new Vector2(-0.25f, 0.25f);          // Hard coded xy value, based on the size of the sprite
            //Flip();
            
        }
        else if (move > 0)      // If player is moving right
        {
            rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
            //transform.localScale = new Vector2(0.25f, 0.25f);           // Hard coded xy value, based on the size of the sprite
            //Flip();
        }
        

        //==========================================
        // ANIMATOR PARAMETERS
        //==========================================
        anim.SetBool("Ground", isGrounded);                   // Player idle
        anim.SetFloat("vSpeed", rb2d.velocity.y);             // Player jump 
        anim.SetFloat("Speed", Mathf.Abs(move));              // Player run
                                                                
    }

    /*void Flip()
    {
        facingLeft = !facingLeft;
        Vector2 theScale = transform.localScale;               // Take the current scale size of object
        theScale.x *= -1;                                      // Reverse the scale to create horizontal 'flip'
        transform.localScale = theScale;                       // Flip the object to the way the players is moving
    }*/

    //==========================================
    // TRIGGER | COLLISION SCRIPTS
    //==========================================
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // COLLECTING ITEMS FOR GREEN BIN                               // Tutorial from: https://www.youtube.com/watch?v=bvlPtAErGp0
        if (collision.CompareTag("FruitCollectable"))                   // Check to see what the item is tagged as
        {
            // Play sound when the fruit has been collected
            AudioManager.PlaySound("CollectCoin");
            
            // Define the type of item that has been collected
            string itemType = collision.gameObject.GetComponent<GreenBinCollectable>().itemType;

            // Save the collected fruit items into a list
            greenBinInventory.Add(itemType);

            print("We have collected a: " + itemType);                  // What item was collected?
            print("Inventory length: " + greenBinInventory.Count);      // What is the list count?

            // Remove the item from the game
            Destroy(collision.gameObject);

        }
    }
}
