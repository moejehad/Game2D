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
    private float moveSpeed = 5f;
    private float jumpForce = 6f;
    private float horizontalMove;
    private bool moveRight;
    private bool moveLeft;
    private bool jump;

    private enum MovementState { idle, running, jumping}
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        moveLeft = false;
        moveRight = false;
        jump = false;
    }

    private void Update()
    {
        Movement();
        rb.velocity = new Vector2(horizontalMove,rb.velocity.y);

    }

    public void pointerDownLeft(){
        moveLeft = true;
    }

    public void pointerUpLeft(){
        moveLeft = false;
    }

    public void pointerDownRight(){
        moveRight = true;
    }

    public void pointerUpRight(){
        moveRight = false;
    } 

    public void pointerDownJump(){
        jump = true;
    }

    public void pointerUpJump(){
        jump = false;
    } 
    

    private void Movement(){

            MovementState state;
            if(moveLeft){
                horizontalMove = -moveSpeed;
                state = MovementState.running;
                sprite.flipX = true;
            }else if(moveRight){
                horizontalMove = moveSpeed;
                state = MovementState.running;
                sprite.flipX = false;
            }else {
                horizontalMove = 0;
                state = MovementState.idle;
            }

            if(jump && IsGrounded()){
                rb.velocity = new Vector3(rb.velocity.x,jumpForce,0);
                state = MovementState.jumping;
            }else if(jump) {
                state = MovementState.jumping;
            }
        
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded(){
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
