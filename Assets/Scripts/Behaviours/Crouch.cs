using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : AbstractBehaviour {

    public float scale = 0.75f;
    public bool crouching;
    public float centerOffsetY = 0;

    private BoxCollider2D boxCollider;
    private Vector2 originalCenter;

    protected override void Awake() {
        base.Awake();

        boxCollider = GetComponent<BoxCollider2D>();
        originalCenter = boxCollider.offset;
    }

    protected virtual void OnCrouch(bool value) {

        crouching = value;

        ToggleScripts(!crouching);

        float height = boxCollider.size.y;

        float newOffsetY;
        float heightReciprocal;

        if (crouching) {
            heightReciprocal = scale;
            newOffsetY = boxCollider.offset.y - height*0.25f + centerOffsetY;
        }
        else {
            heightReciprocal = 1/scale;
            newOffsetY = originalCenter.y;
        }

        height *= heightReciprocal;
        boxCollider.size = new Vector2(boxCollider.size.x, height);
        boxCollider.offset = new Vector2( boxCollider.offset.x, newOffsetY);
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        var canCrouch = inputState.GetButtonValue( inputButtons[0]);
        if (canCrouch && collisionState.standing && !crouching) {
            OnCrouch(true);
        }
        else if (crouching && !canCrouch) {
            OnCrouch(false);
        }
        
    }
}
