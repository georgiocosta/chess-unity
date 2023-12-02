using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    void Start()
    {
        SetUpMoves();
    }

    void SetUpMoves()
    {
        for (int i = 1; i < 9; i++)
        {
            moves.Add(new int[] { i, 0 });
            moves.Add(new int[] { -i, 0 });
            moves.Add(new int[] { 0, i });
            moves.Add(new int[] { 0, -i });
            moves.Add(new int[] { i, i });
            moves.Add(new int[] { -i, i });
            moves.Add(new int[] { i, -i });
            moves.Add(new int[] { -i, -i });
        }
    }
}
