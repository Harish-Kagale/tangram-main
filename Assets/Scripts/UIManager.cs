using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject puzzleCompleteUI;
    public GameObject gameplayUI;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        gameplayUI?.SetActive(true);
        puzzleCompleteUI?.SetActive(false);
    }

    public void ShowPuzzleCompleteUI()
    {
        gameplayUI?.SetActive(false);
        puzzleCompleteUI?.SetActive(true);
    }

    public void RestartPuzzle()
    {
        PuzzleManager.Instance.ResetPuzzle();

        gameplayUI?.SetActive(true);
        puzzleCompleteUI?.SetActive(false);
    }
}
