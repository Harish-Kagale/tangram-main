using UnityEngine;

public class Piece : MonoBehaviour
{
    [Header("Snap Settings")]
    public Transform target;
    public float snapDistance = 0.3f;
    public float rotationTolerance = 10f;

    [Header("Continuous Rotation")]
    public float rotationSpeed = 150f;

    private float baseHeight; // The actual ground level height
    private const float liftAmount = 0.2f; // Adjusted for visibility

    private Camera mainCamera;
    private Vector3 dragOffset;
    private bool isSnapped;

    private Vector3 startPosition;
    private Quaternion startRotation;
    public static Piece selectedPiece;
    private bool isButtonRotating = false;

    void Awake()
    {
        mainCamera = Camera.main;
        startPosition = transform.position;
        startRotation = transform.rotation;
        
        // IMPORTANT: Capture the ground height once at the start
        baseHeight = transform.position.y;
    }

    public void SetButtonRotation(bool rotating) => isButtonRotating = rotating;

    void OnMouseDown()
    {
        if (isSnapped) return;
        selectedPiece = this;
        AudioManager.Instance.PlayPopSfx();
        // Calculate offset based on current lifted position to avoid jumps
        dragOffset = transform.position - GetMouseWorldPosition(transform.position.y);
    }

    void OnMouseDrag()
    {
        if (isSnapped || selectedPiece != this) return;

        // Move piece at the LIFTED height
        Vector3 pos = GetMouseWorldPosition(baseHeight + liftAmount) + dragOffset;
        pos.y = baseHeight + liftAmount; 
        transform.position = pos;
    }

    void OnMouseUp()
    {
        if (isSnapped || selectedPiece != this) return;

        // 1. Drop the piece back to base height physically before snapping
        Vector3 droppedPos = transform.position;
        droppedPos.y = baseHeight;
        transform.position = droppedPos;

        // 2. Now check the snap
        TrySnap();
    }

    void Update()
    {
        if (isSnapped || selectedPiece != this) return;

        if (Input.GetKey(KeyCode.R) || Input.GetMouseButton(1) || isButtonRotating)
        {
            AudioManager.Instance.PlayClickSfx();
            HandleSmoothRotation();
        }
    }

    void HandleSmoothRotation()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void TrySnap()
    {
        // Now that the piece is at baseHeight, the distance check will be accurate
        float distance = Vector3.Distance(transform.position, target.position);
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, target.eulerAngles.y));

        if (distance <= snapDistance && angleDifference <= rotationTolerance)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
            isSnapped = true;
            target.gameObject.SetActive(false);
            PuzzleManager.Instance?.CheckPuzzleComplete();
            AudioManager.Instance.PlaySnappedSfx();
        }
    }

    // Pass the height we want to raycast against
    Vector3 GetMouseWorldPosition(float yCoordinate)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0f, yCoordinate, 0f));
        plane.Raycast(ray, out float enter);
        return ray.GetPoint(enter);
    }

    public void ResetPiece()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        isSnapped = false;
        if(target != null) target.gameObject.SetActive(true);
    }
    public bool IsPlacedCorrectly()
    {
        return isSnapped;
    }
}