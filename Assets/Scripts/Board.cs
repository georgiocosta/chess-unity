using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Square[,] squares = new Square[8,8];

    void Start()
    {
        SetUpBoard();
    }

    private void SetUpBoard()
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                squares[i,j] = new Square(i, j);
                Debug.Log(squares[i,j].GetX() + " " + squares[i, j].GetY());
            }
        }
    }
}
