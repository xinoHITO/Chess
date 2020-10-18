using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public LayerMask Mask;
    private static Camera MainCamera;

    private ChessPiece LastSelectedPiece;
    private ChessPiece ClickedPiece;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ChessPiece selectedPiece = GetSelectedPiece();

        if (ClickedPiece == null)
        {
            HoverPieces(selectedPiece);
        }
        
        ClickPiece(selectedPiece);
    }

    private void HoverPieces(ChessPiece selectedPiece)
    {
        if (selectedPiece == null)
        {
            LastSelectedPiece?.ClearHighlight();
        }
        else
        {
            selectedPiece.HighlightHover();
        }
        LastSelectedPiece = selectedPiece;
    }

    private void ClickPiece(ChessPiece selectedPiece)
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Board.Instance.ClearHighlight();

        ClickedPiece = selectedPiece;
        ClickedPiece?.HighlightClick();
    }

    private ChessPiece GetSelectedPiece()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        ChessPiece piece = null;
        if (Physics.Raycast(ray, out hitInfo, 1000, Mask))
        {
            piece = hitInfo.collider?.GetComponent<ChessPiece>();
        }
        return piece;
    }

}
