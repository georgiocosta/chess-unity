using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public List<int[]> moves = new List<int[]>();
    public List<int[]> validMoves = new List<int[]>();

    public bool isWhite;

    public bool isLinearMover = false;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public List<int[]> GetMoves()
    {
        return moves;
    }

    public void AddLine(int[] direction, int magnitude)
    {
        for(int i = 1; i < magnitude; i++)
        {
            moves.Add(new int[] {direction[0] * i, direction[1] * i});
        }
    }

    public virtual void SetWhite(bool isWhite)
    {
        this.isWhite = isWhite;
        /*
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (isWhite == true)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.black;
        } */
    }

    public bool IsWhite()
    {
        return isWhite;
    }

    public bool IsLinearMover()
    {
        return isLinearMover;
    }

    public void AddValidMove(int[] validMove)
    {
        validMoves.Add(validMove);
    }

    public void ClearValidMoves()
    {
        validMoves.Clear();
    }

    public List<int[]> GetValidMoves()
    {
        return validMoves;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "chess-pawn.png", true);
    }
}
