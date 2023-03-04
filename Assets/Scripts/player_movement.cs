using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public Vector2 move;
    public float moveSpeed = 20;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    }

    // Update is called once per frame
    void Update()
    {
        // get input
        move.x = Input.GetAxisRaw("Horizontal"); 
        move.y = Input.GetAxisRaw("Vertical");
        move.Normalize();        
    }
    void FixedUpdate()
    {
        // adjust object velocity
        body.velocity = body.velocity + move * Time.fixedDeltaTime * moveSpeed;

    }
}
