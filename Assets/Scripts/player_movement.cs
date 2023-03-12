using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public GameObject DeplacementEffectPrefab;
    public Vector2 move = new Vector2 ( 0, 0);
    Rigidbody2D body;
    public double smoothing = 1f;
    Vector3 targetPos;
    public bool canMove = true;
    bool inMove = false;
    Vector3 lockedTarget;
    Quaternion rotation;
    float angle;
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
        move.x = move.x * 0.225f;
        move.y = move.y * 0.225f;
        targetPos = transform.position + new Vector3 (move.x, move.y , 0);

    }
    void FixedUpdate()
    {
        if( transform.position == lockedTarget)
        {
            inMove = false;
        }
        if(canMove & move != new Vector2(0,0))
        {
            // Right or left
            if(move ==  new Vector2(0.225f,0))
            {
                angle = 0;
            }else if(move ==  new Vector2(-0.225f,0))
            {
                angle = 180;
            }
            // Top or down
            else if(move ==  new Vector2(0, -0.225f))
            {
                angle = -90;

            }else if(move ==  new Vector2(0, 0.225f))
            {
                angle = 90;
            }
            // Diagonals
            else if(move ==  new Vector2(0.225f, 0.225f))
            {
                angle = 45;
            }else if(move ==  new Vector2(0.225f, -0.225f))
            {
                angle = -45;
            }else if(move ==  new Vector2(-0.225f, -0.225f)) 
            {
                angle = -135;
            }else if(move ==  new Vector2(-0.225f, 0.225f))
            {
                angle = 135;
            }


            rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if((move ==  new Vector2(-0.225f,0)))
            {
                rotation = rotation * new Quaternion(180,0,0,0);
            }
            Instantiate(DeplacementEffectPrefab, transform.position, rotation);
            inMove = true;
            canMove = false;
            lockedTarget = targetPos;
        }
        if(inMove)
        {
            transform.position = Vector3.Lerp(transform.position, lockedTarget, (float)smoothing * Time.deltaTime);
        }
    }
}
 