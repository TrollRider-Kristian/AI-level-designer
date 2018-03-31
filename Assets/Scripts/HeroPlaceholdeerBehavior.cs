using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlaceholdeerBehavior : MonoBehaviour {
    public float maxSpeed = 10f;
    bool facingRight = true;

    Animator heroPlaceholderAnimator;

	// Use this for initialization
	void Start () {
        heroPlaceholderAnimator = GetComponent<Animator>();
	}
	
	// Because we're dealing with RigidBody2D:
	void FixedUpdate () {
        float move = Input.GetAxis("Horizontal");
        heroPlaceholderAnimator.SetFloat("speed", Mathf.Abs(move));

        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

        if (move > 0 && !facingRight) { Flip(); }
        if (move < 0 && facingRight) { Flip(); }
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
