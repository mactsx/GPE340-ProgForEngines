using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Controller
{
    [HideInInspector] public NavMeshAgent agent;
    public float stoppingDistance;
    public Transform targetTransform;
    private Vector3 desiredVelocity = Vector3.zero;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void PossessPawn(Pawn pawnToPossess)
    {
        // Call the parent function
        base.PossessPawn(pawnToPossess);

        // Get the AI agent component
        agent = pawn.GetComponent<NavMeshAgent>();

        // If there is no component, add one
        if (agent == null)
        {
            agent = pawn.gameObject.AddComponent<NavMeshAgent>();   
        }

        // Set stopping distance
        agent.stoppingDistance = stoppingDistance;

        // Set the speed to the pawn max
        agent.speed = pawn.maxMoveSpeed;

        // Set max rotation to the pawn max
        agent.angularSpeed = pawn.maxRotationSpeed;

        // Disable movement and rotation from nav mesh agent
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    protected override void UnpossessPawn()
    {
        // Remove the nav mesh agent
        Destroy(agent);

        // Update the variables
        base.UnpossessPawn();
    }

    
    protected override void MakeDecsions()
    {
        // If this controller does not have a pawn, do nothing
        if (pawn == null)
        {
            return;
        }

        // Set NavMesh to seek target
        agent.SetDestination(targetTransform.position);

        // Get the desired velocity the ai will move in to follow the path
        desiredVelocity = agent.desiredVelocity;

        // Send that direction to the move function - add speed in the move function
        pawn.Move(desiredVelocity.normalized, pawn.maxMoveSpeed);

        // Look towards the player
        pawn.RotateToLookAt(targetTransform.position);
    }
}
