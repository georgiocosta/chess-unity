using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Transform squaresTransform;

    private Square[,] squares = new Square[8, 8];
    private Square selectedSquare;
    private List<Square> movableSquares;

    [SerializeField]
    private Pawn[] pawns = new Pawn[8];

    void Start()
    {
        AssignSquares();
        SetUpPieces();

        movableSquares = new List<Square>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("Clicked");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (selectedSquare != null)
            {
                selectedSquare.Deselect();

                if(movableSquares.Count > 0)
                {
                    foreach(Square movableSquare in movableSquares)
                    {
                        movableSquare.Deselect();
                    }
                    movableSquares.Clear();
                }
            }
            if (hit.collider != null)
            {
                if (hit.transform.GetComponent<Square>().GetPiece())
                {
                    selectedSquare = hit.transform.GetComponent<Square>();
                    Piece selectedPiece = selectedSquare.GetPiece();
                    List<int[]> moves = selectedPiece.GetMoves();
                    
                    hit.transform.GetComponent<Square>().Select();

                    foreach(int[] move in moves)
                    {
                        if(selectedSquare.GetX() + move[1] <= 8 && selectedSquare.GetX() + move[1] >= 0
                            && selectedSquare.GetY() + move[0] <= 8 && selectedSquare.GetY() + move[0] >= 0)
                        {
                            Square moveSquare = squares[selectedSquare.GetX() + move[1], selectedSquare.GetY() + move[0]];
                            movableSquares.Add(moveSquare);
                            moveSquare.Select();
                        }
                    }
                }
            }
        }
    }

    private void AssignSquares()
    {
        int k = 0;

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                squares[i,j] = squaresTransform.GetChild(k).GetComponent<Square>();
                squares[i, j].SetXY(i, j);

                k++;

                Debug.Log(squares[i,j].GetX() + " " + squares[i, j].GetY());
            }
        }
    }

    private void SetUpPieces()
    {
        for(int i = 0; i < 8; i++)
        {
            squares[1, i].SetPiece(pawns[i]);
        }
    }
}
