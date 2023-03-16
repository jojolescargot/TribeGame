using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PositionToChessPosition : MonoBehaviour
{
    private Camera cam;

    public Vector3  ConvertionChessToPosition(List<int> chessPos)
    {   
        Vector3 Position = Vector3.zero;
        double x = (chessPos[0] - 1 ) * 0.225;
        double y = -1*(chessPos[1] - 1) * 0.225  - 0.0925 ;
        Position.x = (float)x;
        Position.y =  (float)y;
        return(Position);
    }
    public List<int>  ConvertionPositionToChess(Vector3 Position)
    {
            
        List<int> chessPos = new List<int>();  
        double number;
        double letter;
        
        letter = Position.x / 0.225 + 1;
        number = -1* (Position.y  + 0.0925) / 0.225 + 1  ;


        chessPos.Add(Convert.ToInt32(letter));
        chessPos.Add(Convert.ToInt32(number));
        return(chessPos);
    }
    public List<int>  ConvertionMousePositionToChess(Vector3 Position)
    {
        List<int> chessPos = new List<int>();  
        for (int i = 0; i <= 7; i++)         
        {
            for( int h = 0; h <= 7; h++)
            {
                
                Vector3 caseCenter;
                caseCenter.x =  (float)i * (float)0.225;
                caseCenter.y =  (float)h * (float)-0.225;


                if(Position.x < caseCenter.x + 0.1125 && Position.x > caseCenter.x - 0.1125 && Position.y < caseCenter.y + 0.1125 && Position.y > caseCenter.y - 0.1125)
                {

                        chessPos.Add(i+1);
                        chessPos.Add(h+1);
                        return(chessPos);
                }
            }
        }
        chessPos.Add(666);
        chessPos.Add(666);
        return(chessPos);
    }

    void Update()
    {
        cam = Camera.main;


        Vector3 mousePos = Input.mousePosition;
        string listeString = string.Join(", ", ConvertionMousePositionToChess(cam.ScreenToWorldPoint(mousePos)));

        Debug.Log(listeString); 
    }




}
