using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    private Piece piece;

    public int x, y;

    public Piece GetPiece()
    {
        return piece;
    }

    public void SetPiece(Piece newPiece)
    {
        piece = newPiece;
    }
}
