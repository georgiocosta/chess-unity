using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public List<int[]> moves = new List<int[]>();

    public bool isWhite;

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
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (isWhite == true)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.black;
        }
    }

    public bool IsWhite()
    {
        return isWhite;
    }
}
