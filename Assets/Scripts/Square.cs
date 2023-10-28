using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square
{
    private Piece piece;

    private int x, y;

    public Square(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Square(int x, int y, Piece piece)
    {
        this.x = x;
        this.y = y;
        this.piece = piece;
    }

    public Piece GetPiece()
    {
        return piece;
    }

    public void SetPiece(Piece newPiece)
    {
        piece = newPiece;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }
}
