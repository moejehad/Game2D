using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;
    private float moveSpeed = 5f;
    private float jumpForce = 6f;

    private enum MovementState { idle, running, jumping}
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetKey("left shift") && IsGrounded()){
            rb.velocity = new Vector3(rb.velocity.x,jumpForce,0);
        }

        UpdateAnimationState();

    }

private void UpdateAnimationState(){

        MovementState state;
        if(dirX > 0f){
            state = MovementState.running;
            sprite.flipX = false;
        }else if(dirX < 0f){
            state = MovementState.running;
            sprite.flipX = true;
        }else {
            state = MovementState.idle;
        }

        if(rb.velocity.y > .1f){
            state = MovementState.jumping;
        }else if(rb.velocity.y < -.1f) {
            state = MovementState.jumping;
        }
        
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded(){
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
