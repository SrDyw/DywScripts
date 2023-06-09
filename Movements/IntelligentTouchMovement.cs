using UnityEngine;
using UnityEngine.AI;
using System;


[RequireComponent(typeof(NavMeshAgent))]
public class IntelligentTouchMovement : MonoBehaviour
{
    [SerializeField]
    [Header("Stats")]
    private float speedMovement = 10;
    [SerializeField]
    private bool updateRotation = true;
    [SerializeField]
    private float angularSpeed = 120;
    private NavMeshAgent agent;
    [SerializeField]
    [Tooltip("Axis that can cancel the agent functions")]private string[] axisToCheck;


    //< Properties
    public Vector3 Velocity { get => agent.velocity; private set => agent.velocity = value; }
    public bool UpdateRotation { get => updateRotation; set => updateRotation = value; }
    public float SpeedMovement { get => speedMovement; set => speedMovement = value; }
    public float AngularSpeed { get => angularSpeed; set => angularSpeed = value; }


    void Update()
    {
        if (agent != null)
        {
            
            foreach (var axis in axisToCheck)
            {   
                try {
                    if (Input.GetAxis(axis) != 0) {
                        agent.destination = transform.position;
                        return;
                    }
                } catch (ArgumentException ex) {
                    Debug.LogError("Axis doesn't exists!");

                    ex.GetBaseException();
                }
            }

            agent.speed = SpeedMovement * Time.fixedDeltaTime * 25f;
            agent.updateRotation = UpdateRotation;
            agent.angularSpeed = AngularSpeed * 3;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    agent.destination = hit.point;
                }
            }
        }
        else
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }
}
