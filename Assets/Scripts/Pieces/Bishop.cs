using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    void Start()
    {
        SetUpMoves();
        isLinearMover = true;
    }

    void SetUpMoves()
    {
        AddLine(new int[] { 1, 1 }, 8);
        AddLine(new int[] { -1, 1 }, 8);
        AddLine(new int[] { 1, -1 }, 8);
        AddLine(new int[] { -1, -1 }, 8);
    }
}
