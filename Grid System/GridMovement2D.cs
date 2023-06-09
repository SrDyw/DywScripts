using UnityEngine;
using DywFunctions.MathExtras;

enum gm2DState {
    IDLE,
    WALK,
    RUN
}

[RequireComponent(typeof(BoxCollider2D))]
public class GridMovement2D : MonoBehaviour
{
    [SerializeField]
    [Header("Settings")]
    private float gridSize = 1;
    [SerializeField]
    private string horAxis = "Horizontal", verAxis = "Vertical", runAxis = "Fire3";
    [SerializeField]
    private LayerMask collisionable;
    [SerializeField]
    private Vector2 pivotOffset = new Vector2(0.5f, -0.5f);
    [SerializeField]
    [Tooltip("Axis that can cancel the movement")]
    private string[] bloqAxis;

    [SerializeField]
    [Header("Stats")]
    [Range(1, 10)]private float speedMovement = 3;
    [SerializeField]
    [Range(1, 2)]private float sprintFactor = 2;

    new private BoxCollider2D collider2D;
    private Vector2 direction;
    private Vector2 cellTarget;
    private bool isMoving = false;
    private bool isRuuning = false;
    private bool canRun = true;
    private bool canWalk = true;
    private Vector2 velocity;

    private gm2DState stateMovement = gm2DState.IDLE;

    private Vector2 lastDirection = Vector2.down;


    //< Properties
    public float SpeedMovement {get => speedMovement; set => speedMovement = value; }
    public Vector2 Direction { get => direction; set => direction = value; }
    internal gm2DState StateMovement { get => stateMovement; set => stateMovement = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public Vector2 Velocity { get => velocity; set => velocity = value; }
    public Vector2 LastDirection { get => lastDirection; set => lastDirection = value; }
    public float GridSize { get => gridSize; set => gridSize = value; }
    public bool CanRun { get => canRun; set => canRun = value; }
    public bool CanWalk { get => canWalk; set => canWalk = value; }
    public bool IsRuuning { get => isRuuning; set => isRuuning = value; }

    //>
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        cellTarget = transform.position;
    }

    void Update()
    {
        if (IsMoving == false) {
            stateMovement = gm2DState.IDLE;
            velocity = Vector2.zero;
        }

        Movement();
    }

    void Movement() {
        isRuuning = false;
        bool canMove = true;

        foreach (var axis in bloqAxis)
        {
            if (Input.GetButton(axis) | !canWalk) {
                canMove = false;
                break;
            }
            
        }

        float horDirection = (Input.GetAxisRaw(verAxis) == 0)? Input.GetAxisRaw(horAxis) : 0;
        float verDirection = (Input.GetAxisRaw(horAxis) == 0)? Input.GetAxisRaw(verAxis) : 0;

        if (horDirection < 0) horDirection = -1; if (horDirection > 0) horDirection = 1;
        if (verDirection < 0) verDirection = -1; if (verDirection > 0) verDirection = 1;

        float trueVelocity = speedMovement;

        bool placeFree = true;

        direction = new Vector2(horDirection, verDirection);

        if (direction != Vector2.zero) {
            lastDirection = direction;
        }

        if (direction != Vector2.zero) {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position + (Vector3)pivotOffset, direction, gridSize, collisionable);
            placeFree = (hit.Length == 0);

            if (hit.Length != 0) {
                foreach (var h in hit)
                {
                    Collider2D collider = h.collider.GetComponent<Collider2D>();

                    if (collider.isTrigger) {
                        placeFree = true;
                    }
                    else {
                        placeFree = false;
                        break;
                    }
                }
                
            }


            if (placeFree & canMove) {
                if (!IsMoving) {
                    cellTarget = transform.position + (Vector3)direction * gridSize;
                    IsMoving = true;
                    stateMovement = gm2DState.WALK;
                }

                if (!runAxis.Equals("") & canRun) {
                    if (Input.GetAxisRaw(runAxis) != 0) {
                        trueVelocity = speedMovement * sprintFactor;
                        stateMovement = gm2DState.RUN;
                        isRuuning = true;
                    }
                }
            }
        }

        if (Vector2.Distance(transform.position, cellTarget) > 0.2f) {
            velocity = MathE.VectorDirection(transform.position, cellTarget) * trueVelocity * Time.deltaTime;
            transform.position += (Vector3)velocity; 
        }
        else {
            transform.position = cellTarget;
            IsMoving = false;
        }
    }
}
