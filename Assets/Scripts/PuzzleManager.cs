using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public Piece[] pieces;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CheckPuzzleComplete()
    {
        foreach (var piece in pieces)
        {
            if (!piece.IsPlacedCorrectly())
                return;
        }

        Debug.Log("Puzzle Complete!");
        UIManager.Instance.ShowPuzzleCompleteUI();
    }

    
    public void ResetPuzzle()
    {
        foreach (var piece in pieces)
        {
            piece.ResetPiece();
        }
    }
}
