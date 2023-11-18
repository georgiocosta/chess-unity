using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    void Start()
    {
        SetUpMoves();
    }

    void SetUpMoves()
    {
        moves.Add(new int[] {1, 0});
    }
}
