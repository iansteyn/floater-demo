using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionState : MonoBehaviour {

    public LayerMask collisionLayer;
    public bool standing;
    public bool onWall;

    public Vector2 bottomPosition = Vector2.zero;
    public Vector2 rightPosition = Vector2.zero;
    public Vector2 leftPosition = Vector2.zero; 

    public float collisionRadius = 10f;
    public float collisionWidth = 10f;
    public float collisionHeight = 56f;

    public Color debugCollisionColor = Color.red;

    private InputState inputState;

    // Start is called before the first frame update
    void Awake() {
        inputState = GetComponent<InputState>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {

        var pos = bottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        standing = Physics2D.OverlapCircle( pos, collisionRadius, collisionLayer);

        pos = (inputState.direction == Directions.Right) ? rightPosition : leftPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        onWall = Physics2D.OverlapBox( pos, new Vector2(collisionWidth, collisionHeight), 0, collisionLayer);
    }

    void OnDrawGizmos() {
        Gizmos.color = debugCollisionColor;

        var positions = new Vector2[] {rightPosition, bottomPosition, leftPosition};

        foreach (var position in positions) {
            var pos = position;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            if (position.Equals(bottomPosition)) {
                Gizmos.DrawWireSphere( pos, collisionRadius);
            }
            else {
                Gizmos.DrawWireCube( pos, new Vector3(collisionWidth, collisionHeight, 1));
            }
        }
    }
}
