using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : AbstractBehaviour {

    public float jumpSpeed = 200f;
    public float jumpDelay = 0.1f;
    public int jumpCount = 2;

    protected float lastJumpTime = 0;
    protected int jumpsRemaining = 0;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        var canJump = inputState.GetButtonValue( inputButtons[0]);
        var holdTime = inputState.GetButtonHoldTime( inputButtons[0]);

        //maybe clean up this code later??
        if (collisionState.standing) {
            if (canJump && holdTime < 0.1f) {
                jumpsRemaining = jumpCount - 1;
                OnJump();
            }
        }
        else {
            if (canJump && (holdTime < 0.1f) && (Time.time - lastJumpTime > jumpDelay)) {

                if (jumpsRemaining > 0) {
                    OnJump();
                    jumpsRemaining--;
                }
            }
        }
    }

    protected virtual void OnJump() {
        var vel = body2d.velocity;
        lastJumpTime = Time.time;
        body2d.velocity = new Vector2(vel.x, jumpSpeed);
    }
}
