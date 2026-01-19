using UnityEngine;

public class RotationButtonBridge : MonoBehaviour
{
    public void OnPointerDown()
    {
        // Accesses the static selectedPiece from your Piece script
        if (Piece.selectedPiece != null)
        {
            Piece.selectedPiece.SetButtonRotation(true);
        }
    }

    public void OnPointerUp()
    {
        if (Piece.selectedPiece != null)
        {
            Piece.selectedPiece.SetButtonRotation(false);
        }
    }
}