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
    private Piece selectedPiece;

    [SerializeField]
    private Pawn[] pawns = new Pawn[16];
    [SerializeField]
    private Rook[] rooks = new Rook[4];
    [SerializeField]
    private Knight[] knights = new Knight[4];
    [SerializeField]
    private Bishop[] bishops = new Bishop[4];
    [SerializeField]
    private Queen[] queens = new Queen[2];
    [SerializeField]
    private King[] kings = new King[2];

    private bool isWhiteTurn;

    void Start()
    {
        AssignSquares();
        SetUpPieces();

        movableSquares = new List<Square>();
        isWhiteTurn = true;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            
            if (hit.collider != null)
            {
                Square clickedSquare = hit.transform.GetComponent<Square>();

                Debug.Log("Clicked " + clickedSquare.GetX() + ", " + clickedSquare.GetY());

                //Move selected
                if (movableSquares.Find(x => x.name == clickedSquare.name))
                {
                    Debug.Log("Selected move");
                    selectedSquare.SetPiece(null);
                    clickedSquare.SetPiece(selectedPiece);
                    selectedPiece.transform.position = clickedSquare.transform.position;
                    ClearSelection();
                    isWhiteTurn = !isWhiteTurn;
                }
                //Undo selection
                else if (selectedSquare != null)
                {
                    ClearSelection();
                }
                //Piece selected
                if (hit.transform.GetComponent<Square>().GetPiece())
                {
                    if (isWhiteTurn == hit.transform.GetComponent<Square>().GetPiece().IsWhite()) {
                        selectedSquare = hit.transform.GetComponent<Square>();
                        hit.transform.GetComponent<Square>().Select();
                        selectedPiece = selectedSquare.GetPiece();
                        List<int[]> moves = selectedPiece.GetMoves();


                        for (int i = 0; i < moves.Count; i++)
                        {
                            if (selectedSquare.GetX() + moves[i][0] < 8 && selectedSquare.GetX() + moves[i][0] >= 0
                                && selectedSquare.GetY() + moves[i][1] < 8 && selectedSquare.GetY() + moves[i][1] >= 0)
                            {
                                Square moveSquare = squares[selectedSquare.GetX() + moves[i][0], selectedSquare.GetY() + moves[i][1]];
                                if (!moveSquare.GetPiece())
                                {
                                    movableSquares.Add(moveSquare);
                                    moveSquare.Select();
                                }
                                else
                                {
                                    //set a variable m for multiples to skip, then move to next iteration of loop if i % m 
                                    i += (7 - ((i + 1) % 7));
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ClearSelection();
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
        //White pieces
        for(int i = 0; i < 8; i++)
        {
            squares[1, i].SetPiece(pawns[i]);
            pawns[i].SetWhite(true);
        }
        squares[0, 0].SetPiece(rooks[0]);
        squares[0, 1].SetPiece(knights[0]);
        squares[0, 2].SetPiece(bishops[0]);
        squares[0, 3].SetPiece(queens[0]);
        squares[0, 4].SetPiece(kings[0]);
        squares[0, 5].SetPiece(bishops[1]);
        squares[0, 6].SetPiece(knights[1]);
        squares[0, 7].SetPiece(rooks[1]);
        for (int i = 0; i < 8; i++)
        {
            squares[0, i].GetPiece().SetWhite(true);
        }

        //Black pieces
        for (int i = 8; i < 16; i++)
        {
            squares[6, i - 8].SetPiece(pawns[i]);
            pawns[i].SetWhite(false);
        }
        squares[7, 0].SetPiece(rooks[2]);
        squares[7, 1].SetPiece(knights[2]);
        squares[7, 2].SetPiece(bishops[2]);
        squares[7, 3].SetPiece(queens[1]);
        squares[7, 4].SetPiece(kings[1]);
        squares[7, 5].SetPiece(bishops[3]);
        squares[7, 6].SetPiece(knights[3]);
        squares[7, 7].SetPiece(rooks[3]);
        for (int i = 0; i < 8; i++)
        {
            squares[7, i].GetPiece().SetWhite(false);
        }
    }

    private void ClearSelection()
    {
        selectedSquare.Deselect();

        if (movableSquares.Count > 0)
        {
            foreach (Square movableSquare in movableSquares)
            {
                movableSquare.Deselect();
            }
            movableSquares.Clear();
            selectedPiece = null;
        }
    }
}
