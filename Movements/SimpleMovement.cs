using UnityEngine;
using DywFunctions.MathExtras;


[RequireComponent(typeof(Rigidbody))]
public class SimpleMovement : MonoBehaviour
{
    [SerializeField]
    [Header("Input")]
    [Tooltip("Axis tag for the horizontal axis")]
    private string horizontalAxis = "Horizontal";

    [SerializeField]
    [Tooltip("Axis tag for the vertical axis")]
    private string verticalAxis = "Vertical";


    [Header("Stats")]
    [SerializeField]
    private float speedMovement = 10;
    [SerializeField]
    private float angularSpeed = 5;

    [SerializeField]
    private bool canRotate = true;

    //< Referencias
    new private Rigidbody rigidbody;
    //>
    private float angle = 0;

    private Vector2 inputVector;

    //< Properties
    public Vector3 Velocity {get => rigidbody.velocity; set => rigidbody.velocity = value; }
    public float SpeedMovement {get => speedMovement; set => speedMovement = value; }
    public bool CantRotate {get => canRotate; set => canRotate = value; }
    //>
    void Update()
    {
        if (rigidbody == null) {
            rigidbody = GetComponent<Rigidbody>();
        }
        else {
            Movement();
        }
    }

    void Movement() {
        
        inputVector = new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));

        rigidbody.velocity = new Vector3(inputVector.x, 0, inputVector.y) * speedMovement * Time.fixedDeltaTime * 25f; 

        if (Input.GetAxisRaw(horizontalAxis) != 0 | Input.GetAxisRaw(verticalAxis)  != 0) {
            if (canRotate)
                UpdateRotation();
        }
    }

    void UpdateRotation() {
        float angleDestiny = 0;
        float trasitionTime = 0;

        angleDestiny = -MathE.GetAngleFromVector(MathE.VectorDirection(inputVector)) + 90;
        trasitionTime = Time.deltaTime * angularSpeed;

        angle = MathE.LerpAngle(angle, angleDestiny, trasitionTime);
        transform.eulerAngles = new Vector3(0, angle, 0);
    }
}
