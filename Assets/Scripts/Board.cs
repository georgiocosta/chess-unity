using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Transform squaresTransform;

    private Square[,] squares = new Square[8,8];

    [SerializeField]
    private Pawn[] pawns = new Pawn[8];

    void Start()
    {
       AssignSquares();
       SetUpPieces();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("Clicked");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                if (hit.transform.GetComponent<Square>())
                {
                    hit.transform.GetComponent<Square>().OnClicked();
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
