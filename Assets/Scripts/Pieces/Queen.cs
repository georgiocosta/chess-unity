using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    void Start()
    {
        SetUpMoves();
        isLinearMover = true;
    }

    void SetUpMoves()
    {
        AddLine(new int[] { 1, 0 }, 8);
        AddLine(new int[] { -1, 0 }, 8);
        AddLine(new int[] { 0, 1 }, 8);
        AddLine(new int[] { 0, -1 }, 8);
        AddLine(new int[] { 1, 1 }, 8);
        AddLine(new int[] { -1, 1 }, 8);
        AddLine(new int[] { 1, -1 }, 8);
        AddLine(new int[] { -1, -1 }, 8);
    }
}
