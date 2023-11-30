using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    void Start()
    {
        SetUpMoves();
    }

    void SetUpMoves()
    {
        for (int i = 1; i < 9; i++)
        {
            moves.Add(new int[] { i, i });
            moves.Add(new int[] { -i, i });
            moves.Add(new int[] { i, -i });
            moves.Add(new int[] { -i, -i });
        }
    }
}
