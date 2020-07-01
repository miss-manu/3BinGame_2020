using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables to allow our player to move left and right
    public float speed = 3f;                        // Maximum speed for robot
    private float move = 0f;                        // Value to hold if our robot is moving
    private Rigidbody2D rb2d;                       // Value to represent our rigidbody
    private Animator anim;                          // Value to represent our Animator
    //private bool facingLeft = true;                 // Value to represent if we are facing left or right

    // Variables to allow our player to jump and land
    public Transform groundPos;                     // Part of the player that would be grounded
    public float jumpForce;                         // Jump force for player
    private bool isGrounded = false;                // To hold the value returned from 2D overlapcircle
    public float checkRadius;                       // Radius around the check point, to ensure it is touching the ground
    public LayerMask whatIsGround;                  // To check if the player is on the 'ground'

    //private bool isJumping;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();         //Set reference component
        anim = GetComponent<Animator>();            //Set ref Animator component
    }


    void FixedUpdate()
    {
        //==========================================
        // MOVEMENT SCRIPTS
        //==========================================

        // Check first to see if the player is grounded, can only jump from ground
        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);

        // Jump player
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            //isJumping = true;
            //rb2d.AddForce(new Vector2(rb2d.velocity.x, jumpForce));    
            rb2d.velocity = Vector2.up * jumpForce;
            
        }

        // Get input value of horizontal axes to find if player is moving left (-1) or right (1)
        move = Input.GetAxis("Horizontal");     
        
        if (move < 0)       // If player is moving left
        {
            rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y); // Move the player on x axis, keep y axis at same value
            transform.eulerAngles = new Vector3(0, 180, 0);           // Euler angles to change the sprites rotation position
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
        /*else        // If player is not moving
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }*/


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
        Vector2 theScale = transform.localScale;                // Take the current scale size of object
        theScale.x *= -1;                                       // Reverse the scale to create horizontal 'flip'
        transform.localScale = theScale;                        // Flip the object to the way the players is moving
    }*/
}
