using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField]
    private Piece piece;
    [SerializeField]
    private GameObject highlight;

    private int x, y;

    public Piece GetPiece()
    {
        return piece;
    }

    public void SetPiece(Piece newPiece)
    {
        piece = newPiece;
    }

    public void SetXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int GetX()
    {
        return x;
    }
    public int GetY()
    {
        return y;
    }

    public void SetHighlight(bool isSet)
    {
        highlight.SetActive(isSet);
    }

    public void Capture()
    {
        Debug.Log("Capture");
        Destroy(piece.gameObject);
        SetPiece(null);
    }
}
