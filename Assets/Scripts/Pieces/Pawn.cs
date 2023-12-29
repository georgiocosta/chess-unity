using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    private bool isFirstMove;

    void Awake()
    {
        SetUpMoves();
        isFirstMove = true;
    }

    void SetUpMoves()
    {
        moves.Add(new int[] {1, 0});
        moves.Add(new int[] {2, 0});
        moves.Add(new int[] { 1, 1 });
        moves.Add(new int[] { 1, -1});
    }

    public override void SetWhite(bool isWhite)
    {
        base.SetWhite(isWhite);

        if (isWhite == false)
        {
            moves[0] = new int[] { -1, 0 };
            moves[1] = new int[] { -2, 0};
            moves[2] = new int[] { -1, 1 };
            moves[3] = new int[] { -1, -1 };
        }
    }

    public bool IsFirstMove()
    {
        return isFirstMove;
    }

    public void MakeFirstMove() {
        isFirstMove = false;
    }
}
