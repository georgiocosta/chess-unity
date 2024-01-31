using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Transform squaresTransform;

    private Square[,] squares = new Square[8, 8];
    private Square selectedSquare;
    private List<Square> movableSquares;
    private Square[] kingSquares = new Square[2];
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
    private bool check;

    void Start()
    {
        AssignSquares();
        SetUpPieces();

        movableSquares = new List<Square>();
        isWhiteTurn = true;
        check = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            
            if (hit.collider != null)
            {
                Square targetSquare = hit.transform.GetComponent<Square>();

                //Debug.Log("Clicked " + targetSquare.GetX() + ", " + targetSquare.GetY());

                //Move selected
                if (movableSquares.Find(x => x.name == targetSquare.name))
                {
                    //Capture
                    if (targetSquare.GetPiece() != null)
                    {
                        if (targetSquare.GetPiece().IsWhite() != selectedPiece.IsWhite())
                        {
                            targetSquare.Capture();
                        }
                    }
                    //Regular move
                    Debug.Log("Selected move");
                    if(selectedPiece.GetType() == typeof(Pawn))
                    {
                        Pawn selectedPawn = (Pawn)selectedPiece;
                        if (selectedPawn.IsFirstMove())
                        {
                            selectedPawn.MakeFirstMove();
                        }
                    }
                    else if(selectedPiece.GetType() == typeof(King))
                    {
                        kingSquares[selectedPiece.isWhite ? 0 : 1] = selectedSquare;
                    }
                    //Movement
                    selectedSquare.SetPiece(null);
                    targetSquare.SetPiece(selectedPiece);
                    selectedPiece.transform.position = targetSquare.transform.position;
                    ClearSelection();
                    isWhiteTurn = !isWhiteTurn;
                    check = false;
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
                        //Determining check before allowing selection
                        King king = isWhiteTurn ? kings[0] : kings[1];

                        if (!IsSafeSquare(isWhiteTurn, kingSquares[isWhiteTurn ? 0 : 1]))
                        {
                            Debug.Log("CHECK, white = " + isWhiteTurn);
                            check = true;
                        }

                        selectedSquare = hit.transform.GetComponent<Square>();
                        hit.transform.GetComponent<Square>().SetHighlight(true);
                        selectedPiece = selectedSquare.GetPiece();
                        List<int[]> moves = selectedPiece.GetMoves();

                        if(check == true && selectedPiece.GetType() != typeof(King))
                        {
                            return;
                        }

                        if (selectedPiece.GetType() != typeof(Pawn) && selectedPiece.GetType() != typeof(King))
                        {
                            for (int i = 0; i < moves.Count; i++)
                            {
                                if (selectedSquare.GetX() + moves[i][0] < 8 && selectedSquare.GetX() + moves[i][0] >= 0
                                    && selectedSquare.GetY() + moves[i][1] < 8 && selectedSquare.GetY() + moves[i][1] >= 0)
                                {

                                    Square moveSquare = squares[selectedSquare.GetX() + moves[i][0], selectedSquare.GetY() + moves[i][1]];

                                    if (!moveSquare.GetPiece() || moveSquare.GetPiece().IsWhite() != selectedPiece.IsWhite())
                                    {
                                        AddMovableSquare(moveSquare);
                                    }

                                    if (moveSquare.GetPiece() && selectedPiece.IsLinearMover())
                                    {
                                        //set a variable m for multiples to skip, then move to next iteration of loop if i % m 
                                        i += (7 - ((i + 1) % 7));
                                    }
                                }
                            }
                        }
                        else if (selectedPiece.GetType() == typeof(Pawn))
                        {
                            Pawn selectedPawn = (Pawn)selectedPiece;
                            List<Square> moveSquares = new List<Square>();

                            for (int i = 0; i < moves.Count; i++)
                            {
                                if (selectedSquare.GetX() + moves[i][0] < 8 && selectedSquare.GetX() + moves[i][0] >= 0
                                    && selectedSquare.GetY() + moves[i][1] < 8 && selectedSquare.GetY() + moves[i][1] >= 0)
                                { 
                                    moveSquares.Add(squares[selectedSquare.GetX() + moves[i][0], selectedSquare.GetY() + moves[i][1]]);
                                }
                            }

                            if (!moveSquares[0].GetPiece()) {
                                AddMovableSquare(moveSquares[0]);
                                
                                if (selectedPawn.IsFirstMove())
                                {
                                    AddMovableSquare(moveSquares[1]);
                                }
                            }

                            for (int i = 2; i < moveSquares.Count; i++)
                            {
                                if (moveSquares[i].GetPiece())
                                {
                                    if (moveSquares[i].GetPiece().IsWhite() != selectedPiece.IsWhite())
                                    {
                                        AddMovableSquare(moveSquares[i]);
                                    }
                                }
                            }
                        }
                        else if(selectedPiece.GetType() == typeof(King))
                        {
                            for (int i = 0; i < moves.Count; i++)
                            {
                                if (selectedSquare.GetX() + moves[i][0] < 8 && selectedSquare.GetX() + moves[i][0] >= 0
                                    && selectedSquare.GetY() + moves[i][1] < 8 && selectedSquare.GetY() + moves[i][1] >= 0)
                                {

                                    Square moveSquare = squares[selectedSquare.GetX() + moves[i][0], selectedSquare.GetY() + moves[i][1]];

                                    if (IsSafeSquare(selectedPiece.isWhite, moveSquare) && (!moveSquare.GetPiece() || moveSquare.GetPiece().IsWhite() != selectedPiece.IsWhite()))
                                    {
                                        AddMovableSquare(moveSquare);
                                    }
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

    private void AddMovableSquare(Square movableSquare)
    {
        movableSquares.Add(movableSquare);
        movableSquare.SetHighlight(true);
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
        kingSquares[0] = squares[0, 4];
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
        kingSquares[1] = squares[7, 4];
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
        selectedSquare.SetHighlight(false);

        if (movableSquares.Count > 0)
        {
            foreach (Square movableSquare in movableSquares)
            {
                movableSquare.SetHighlight(false);
            }
            movableSquares.Clear();
            selectedPiece = null;
        }
    }

    private bool IsSafeSquare(bool isWhite, Square square)
    {
        int x = square.GetX();
        int y = square.GetY();

        //Horizontal check
        for(int i = x + 1; i < 8; i++)
        {
            if(squares[i, y].GetPiece())
            {
                Piece piece = squares[i, y].GetPiece();

                if (piece.isWhite == isWhite)
                {
                    break;
                }
                else if (i == x + 1 && piece.GetType() == typeof(King))
                {
                    return false;
                }
                else if(piece.GetType() == typeof(Rook) || piece.GetType() == typeof(Queen))
                {
                    return false;
                }
                else
                {
                    break;
                }
            }
        }

        for (int i = x - 1; i > 0; i--)
        {
            if (squares[i, y].GetPiece())
            {
                Piece piece = squares[i, y].GetPiece();

                if(piece.isWhite == isWhite)
                {
                    break;
                }
                else if (i == x - 1 && piece.GetType() == typeof(King))
                {
                    return false;
                }
                else if (piece.GetType() == typeof(Rook) || piece.GetType() == typeof(Queen))
                {
                    return false;
                }
                else
                {
                    break;
                }
            }
        }

        //Vertical check
        for (int i = y + 1; i < 8; i++)
        {
            if (squares[x, i].GetPiece())
            {
                Piece piece = squares[x, i].GetPiece();

                if (piece.isWhite == isWhite)
                {
                    break;
                }
                else if (i == y + 1 && piece.GetType() == typeof(King))
                {
                    return false;
                }
                else if (piece.GetType() == typeof(Rook) || piece.GetType() == typeof(Queen))
                {
                    return false;
                }
                else
                {
                    break;
                }
            }
        }

        for (int i = y - 1; i > 0; i--)
        {
            if (squares[x, i].GetPiece())
            {
                Piece piece = squares[x, i].GetPiece();

                if (piece.isWhite == isWhite)
                {
                    break;
                }
                else if (i == y - 1 && piece.GetType() == typeof(King))
                {
                    return false;
                }
                else if (piece.GetType() == typeof(Rook) || piece.GetType() == typeof(Queen))
                {
                    return false;
                }
                else
                {
                    break;
                }
            }
        }
        //Diagonal check
        for (int i = 1; x + i < 8 && y + i < 8; i++)
        {
            if (squares[x + i, y + i].GetPiece())
            {
                Piece piece = squares[x + i, y + i].GetPiece();

                if (piece.isWhite == isWhite)
                {
                    break;
                }
                else if (isWhite == true && i == 1 && piece.GetType() == typeof(Pawn))
                {
                    return false;
                }
                else if (i == 1 && piece.GetType() == typeof(King))
                {
                    return false;
                }
                else if (piece.GetType() == typeof(Bishop) || piece.GetType() == typeof(Queen))
                {
                    return false;
                }
                else
                {
                    break;
                }
            }
        }

        for (int i = 1; x - i > 0 && y + i < 8; i++)
        {
            if (squares[x - i, y + i].GetPiece())
            {
                Piece piece = squares[x - i, y + i].GetPiece();

                if (piece.isWhite == isWhite)
                {
                    break;
                }
                else if (isWhite == false && i == 1 && piece.GetType() == typeof(Pawn))
                {
                    return false;
                }
                else if (i == 1 && piece.GetType() == typeof(King))
                {
                    return false;
                }
                else if (piece.GetType() == typeof(Bishop) || piece.GetType() == typeof(Queen))
                {
                    return false;
                }
                else
                {
                    break;
                }
            }
        }

        for (int i = 1; x + i < 8 && y - i > 0; i++)
        {
            if (squares[x + i, y - i].GetPiece())
            {
                Piece piece = squares[x + i, y - i].GetPiece();

                if (piece.isWhite == isWhite)
                {
                    break;
                }
                else if (isWhite == true && i == 1 && piece.GetType() == typeof(Pawn))
                {
                    return false;
                }
                else if (i == 1 && piece.GetType() == typeof(King))
                {
                    return false;
                }
                else if (piece.GetType() == typeof(Bishop) || piece.GetType() == typeof(Queen))
                {
                    return false;
                }
                else
                {
                    break;
                }
            }
        }

        for (int i = 1; x - i > 0 && y - i > 0; i++)
        {
            if (squares[x - i, y - i].GetPiece())
            {
                Piece piece = squares[x - i, y - i].GetPiece();

                if (piece.isWhite == isWhite)
                {
                    break;
                }
                else if (isWhite == false && i == 1 && piece.GetType() == typeof(Pawn))
                {
                    return false;
                }
                else if (i == 1 && piece.GetType() == typeof(King))
                {
                    return false;
                }
                else if (piece.GetType() == typeof(Bishop) || piece.GetType() == typeof(Queen))
                {
                    return false;
                }
                else
                {
                    break;
                }
            }
        }

        //Knight check
        List<int[]> knightMoves = knights[0].GetMoves(); 

        foreach(int[] move in knightMoves)
        {
            if(x + move[0] < 8 && y + move[1] < 8 && x + move[0] >= 0 && y + move[1] >= 0)
            {
                if(squares[x + move[0], y + move[1]].GetPiece())
                {
                    Piece piece = squares[x + move[0], y + move[1]].GetPiece();

                    if (piece.isWhite != isWhite && piece.GetType() == typeof(Knight))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}
