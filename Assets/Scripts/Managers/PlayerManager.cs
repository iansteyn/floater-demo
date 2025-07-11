using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private InputState inputState;
    private Walk walkBehaviour;
    private Animator animator;
    private CollisionState collisionState;
    private Crouch crouchBehaviour;

    void Awake() {
        inputState = GetComponent<InputState>();
        walkBehaviour = GetComponent<Walk>();
        animator = GetComponent<Animator>();
        collisionState = GetComponent<CollisionState>();
        crouchBehaviour = GetComponent<Crouch>();
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if ((inputState.absVelX < 7*Mathf.Exp(-19)) && collisionState.standing) {
            ChangeAnimationState(0); //0=idle
        }
        
        if ((inputState.absVelX > 7*Mathf.Exp(-19)) && collisionState.standing) {
            if (walkBehaviour.running) {
                ChangeAnimationState(2); //2=run
            }
            else {
                ChangeAnimationState(1); //1=walk
            }
        }

        if (inputState.velY > 1.5*Mathf.Exp(-5)) {
            ChangeAnimationState(3); //3=jump up
        }
        else if (inputState.velY < -1.5*Mathf.Exp(-5)) {
            ChangeAnimationState(4); //4=jump down
        }

        if (crouchBehaviour.crouching) {
            ChangeAnimationState(5); //5=crouch
        }


    }

    void ChangeAnimationState( int value) {
        animator.SetInteger( "AnimState", value);
    }
}
