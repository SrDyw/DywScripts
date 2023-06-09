using UnityEngine;

[ExecuteInEditMode]
public class GridSnap : MonoBehaviour
{
    public Vector2 pivot;
    [SerializeField]
    private LayerMask solid;

    private Vector3 lastPosition;
    void Start()
    {
        Snap();
    }   

    void Update()
    {
        if (transform.position != lastPosition) 
        {
            Snap();
        }
    }

    public void Snap() 
    {
        Vector2 positionPivot = transform.position - (Vector3)pivot;

        float snapX = Mathf.Round((positionPivot.x / GridController.gridSize)) * GridController.gridSize;
        float snapY = Mathf.Round((positionPivot.y / GridController.gridSize)) * GridController.gridSize;

        positionPivot = new Vector2(snapX, snapY);

        transform.position = positionPivot + pivot;
        lastPosition = transform.position;
    }
}
