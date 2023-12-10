using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    void Start()
    {
        SetUpMoves();
    }

    void SetUpMoves()
    {
        for (int i = 1; i < 9; i++)
        {
            AddLine(new int[] { 1, 0 }, 8);
            AddLine(new int[] { -1, 0 }, 8);
            AddLine(new int[] { 0, 1 }, 8);
            AddLine(new int[] { 0, -1 }, 8);
        }
    }
}
