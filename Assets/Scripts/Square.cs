using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField]
    private Piece piece;
    [SerializeField]
    private GameObject highlight;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private SpriteRenderer pieceSprite;

    private int x, y;

    public Piece GetPiece()
    {
        return piece;
    }

    public void SetPiece(Piece newPiece)
    {
        piece = newPiece;

        if (newPiece == null)
        {
            pieceSprite.sprite = null;
            return;
        }

        if (newPiece.isWhite == true)
        {
            pieceSprite.color = Color.white;
        }
        else
        {
            pieceSprite.color = Color.black;
        }

        if(newPiece.GetType() == typeof(Pawn))
        {
            pieceSprite.sprite = sprites[0];
        }
        else if(newPiece.GetType() == typeof(Rook))
        {
            pieceSprite.sprite = sprites[1];
        }
        else if (newPiece.GetType() == typeof(Knight))
        {
            pieceSprite.sprite = sprites[2];
        }
        else if (newPiece.GetType() == typeof(Bishop))
        {
            pieceSprite.sprite = sprites[3];
        }
        else if (newPiece.GetType() == typeof(King))
        {
            pieceSprite.sprite = sprites[4];
        }
        else if (newPiece.GetType() == typeof(Queen))
        {
            pieceSprite.sprite = sprites[5];
        }
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
