using UnityEngine;
using System;

public enum State {
    Idle,
    Walk,
    Run
}


[RequireComponent(typeof(Rigidbody2D))]
public class SimpleMovement2D : MonoBehaviour
{
    [SerializeField]
    [Header("Input")]
    [Tooltip("Axis tag for the horizontal axis")]
    private string horizontalAxis = "Horizontal";

    [SerializeField]
    [Tooltip("Axis tag for the vertical axis")]
    private string verticalAxis = "Vertical";

    [SerializeField]
    [Tooltip("Axis tag for run mode active")]
    private string sprintAxis = "Fire3";


    [Header("Stats")]
    [SerializeField]
    private float speedMovement = 10;
    [SerializeField]
    private float sprintScaler = 2;

    //< Referencias
    new private Rigidbody2D rigidbody;
    //>

    private Vector2 inputVector;
    private State state = State.Idle;

    //< Properties
    public Vector3 Velocity {get => rigidbody.velocity; set => rigidbody.velocity = value; }
    public float SpeedMovement {get => speedMovement; set => speedMovement = value; }
    public float SprintScaler {get => sprintScaler; set => sprintScaler = value; }
    internal State StateMovement { get => state; set => state = value; }
    //>

    void Update()
    {
        if (rigidbody == null) {
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.gravityScale = 0;
        }
        else {
            Movement();
        }

    }

    void Movement() {
        StateControler();

        float realVelocity = speedMovement;

        if (!sprintAxis.Equals(""))
            try {
                if (Input.GetAxisRaw(sprintAxis) != 0) {
                    realVelocity = speedMovement * sprintScaler;
                    state = State.Run;
                }
            } catch (Exception ex) {
                print(ex.Message);
            }

        
        inputVector = new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis)).normalized;

        rigidbody.velocity = new Vector2(inputVector.x, inputVector.y) * realVelocity * Time.fixedDeltaTime * 25f; 

    }

    void StateControler() {
        if (rigidbody.velocity == Vector2.zero) {
            state = State.Idle;
        }
        else {
            state = State.Walk;
        }
    }


}
