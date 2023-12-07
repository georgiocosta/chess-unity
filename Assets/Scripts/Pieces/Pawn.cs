using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    void Awake()
    {
        SetUpMoves();
    }

    void SetUpMoves()
    {
        moves.Add(new int[] {1, 0});
    }

    public override void SetWhite(bool isWhite)
    {
        base.SetWhite(isWhite);

        if (isWhite == false)
        {
            moves[0] = new int[] { -1, 0 };
        }
    }
}
