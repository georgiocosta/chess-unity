using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    void Start()
    {
        SetUpMoves();
    }

    void SetUpMoves()
    {
        moves.Add(new int[] { 1, 2 });
        moves.Add(new int[] { 1, -2 });
        moves.Add(new int[] { 2, 1 });
        moves.Add(new int[] { 2, -1 });
        moves.Add(new int[] { -1, 2 });
        moves.Add(new int[] { -1, -2 });
        moves.Add(new int[] { -2, 1 });
        moves.Add(new int[] { -2, -1 });
    }
}
