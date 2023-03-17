using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class player_movement_mouse : MonoBehaviour
{
    public GameObject DeplacementEffectPrefab;
    public double smoothing = 1f;
    Vector3 targetPos;
    public bool canMove = false;
    bool inMove = false;
    Vector3 lockedTarget;
    Quaternion rotation;
    float angle;
    float x;
    float y;
    public float frameNeedForMovement = 10;
    int frameCounter = 0;
    public Vector2 move;
    Camera cam;
    List<int> chessPos = new List<int>(); 
    List<int> mouseChessPos = new List<int>(); 
    PositionToChessPosition PositionToChess;


    // Start is called before the first frame update
    void Start()
    {
        PositionToChess = gameObject.GetComponent<PositionToChessPosition>(); 
        cam = Camera.main;
        chessPos = PositionToChess.ConvertionPositionToChess(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // get input

        chessPos = PositionToChess.ConvertionPositionToChess(transform.position);
        Vector3 mousePos = Input.mousePosition;
        mouseChessPos = PositionToChess.ConvertionMousePositionToChess(cam.ScreenToWorldPoint(mousePos));
        if(Input.GetMouseButtonDown(0))
        {
            if(Math.Abs(mouseChessPos[0] - chessPos[0]) <= 1 && Math.Abs(mouseChessPos[1] - chessPos[1]) <= 1)
            {
                
                canMove = true;
                targetPos = PositionToChess.ConvertionChessToPosition(mouseChessPos);
            }
        }



    }
    

    void FixedUpdate()
    {

        if( transform.position == lockedTarget )
        {

            inMove = false;
            move = Vector2.zero;
            lockedTarget = Vector3.zero;
            targetPos =    Vector3.zero;
            canMove = true;

        }
        
        if(canMove && targetPos != Vector3.zero)
        {
           
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