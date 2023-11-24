using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    void Start()
    {
        SetUpMoves();
    }

    void SetUpMoves()
    {
        moves.Add(new int[] { 1, 0 });
        moves.Add(new int[] { -1, 0 });
        moves.Add(new int[] { 0, 1 });
        moves.Add(new int[] { 0, -1 });
        moves.Add(new int[] { 1, 1 });
        moves.Add(new int[] { -1, 1 });
        moves.Add(new int[] { 1, -1 });
        moves.Add(new int[] { -1, -1 });
    }
}
