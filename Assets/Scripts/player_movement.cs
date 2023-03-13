using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public GameObject DeplacementEffectPrefab;
    public double smoothing = 1f;
    Vector3 targetPos;
    public bool canMove = true;
    bool inMove = false;
    Vector3 lockedTarget;
    Quaternion rotation;
    float angle;
    float x;
    float y;
    public float frameNeedForMovement = 10;
    int frameCounter = 0;
    public Vector2 move;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // get input
        if(Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Vertical") == -1 || Input.GetAxisRaw("Horizontal") == -1 && !inMove)
        {
            frameCounter += 1;
        }
        if (frameCounter == frameNeedForMovement)
        {
            move.x = Input.GetAxisRaw("Horizontal"); 
            move.y = Input.GetAxisRaw("Vertical");
            move.x = move.x * 0.225f;
            move.y = move.y * 0.225f;
            targetPos = transform.position + new Vector3 (move.x, move.y , 0);
            frameCounter = 0;
        }
        



    }
    

    void FixedUpdate()
    {

        if( transform.position == lockedTarget)
        {
            inMove = false;
            move = Vector2.zero;
            lockedTarget = Vector3.zero;
            targetPos =    Vector3.zero;
            canMove = true;

        }
        
        if(canMove & move != new Vector2(0,0))
        {
            // Right or left
            if(move ==  new Vector2(1,0))
            {
                angle = 0;
            }else if(move ==  new Vector2(-1,0))
            {
                angle = 180;
            }
            // Top or down
            else if(move ==  new Vector2(0, -1))
            {
                angle = -90;

            }else if(move ==  new Vector2(0, 1))
            {
                angle = 90;
            }
            // Diagonals
            else if(move ==  new Vector2(1, 1))
            {
                angle = 45;
            }else if(move ==  new Vector2(1, -1))
            {
                angle = -45;
            }else if(move ==  new Vector2(-1, -1)) 
            {
                angle = -135;
            }else if(move ==  new Vector2(-1,1))
            {
                angle = 135;
            }


            rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if((targetPos ==  new Vector3(-0.225f,0)))
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
 