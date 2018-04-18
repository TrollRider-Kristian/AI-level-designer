using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlaceholderBehavior : MonoBehaviour {
    public float maxSpeed = 10f;
    public float jumpForce = 500f;
    public bool allow_doublejump = false;
    bool facingRight = true;
    int jumps_in_air = 0;

    Animator heroPlaceholderAnimator;

    //using this tutorial for jumping as well: https://unity3d.com/learn/tutorials/topics/2d-game-creation/2d-character-controllers
    bool am_i_on_ground = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    // Use this for initialization
    void Start () {
        heroPlaceholderAnimator = GetComponent<Animator>();
	}
	
	// Because we're dealing with RigidBody2D:
	void FixedUpdate () {
        am_i_on_ground = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        heroPlaceholderAnimator.SetBool("Ground", am_i_on_ground);

        if (am_i_on_ground) jumps_in_air = 0;

        float vMove = Input.GetAxis("Vertical");
        heroPlaceholderAnimator.SetFloat("Vertical", Mathf.Abs(vMove));

        float move = Input.GetAxis("Horizontal");
        heroPlaceholderAnimator.SetFloat("speed", Mathf.Abs(move));

        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

        if (move > 0 && !facingRight) { Flip(); }
        if (move < 0 && facingRight) { Flip(); }
	}

    // Update is called once per frame
    void Update()
    {
        //check if we've fallen too far
        if (gameObject.transform.position.y < -37.0f)
            gameObject.transform.position = new Vector3(0.1f, 0.1f, 0.1f);

        //TODO: should I allow the character to change movement style while in the air (y/n)?

        if ((am_i_on_ground || (allow_doublejump && jumps_in_air < 1)) && (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow))))
        {
            heroPlaceholderAnimator.SetBool("Ground", false);
            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
            jumps_in_air++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Default movement.");
            maxSpeed = 7.0f;
            jumpForce = 500.0f;
            allow_doublejump = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Not sure if pogo stick dude or Luigi?");
            maxSpeed = 4.2f;
            jumpForce = 780.0f;
            allow_doublejump = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Ninja mode!");
            maxSpeed = 13.4f;
            jumpForce = 195.0f;
            allow_doublejump = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Rocket Man!");
            maxSpeed = 2.02f;
            jumpForce = 620.0f;
            allow_doublejump = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Default w/ two half jumps");
            maxSpeed = 6.7f;
            jumpForce = 262.0f;
            allow_doublejump = true;
        }
    }

    // Using a trick for moving left without needing an animation for it according to:
    // https://unity3d.com/learn/tutorials/topics/2d-game-creation/2d-character-controllers
    void Flip() {
        facingRight = !facingRight;
        Vector3 scaleToFlip = transform.localScale;
        scaleToFlip.x *= -1;
        transform.localScale = scaleToFlip;
    }
}
