using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidPawn : Pawn
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Move(Vector3 direction)
    {
        // Convert the direction from world space to local space
        direction = transform.InverseTransformDirection(direction);

        direction *= maxMoveSpeed;

        animator.SetFloat("Forward", direction.z);
        animator.SetFloat("Right", direction.x);
    }

    public override void Rotate(float speed)
    {
        transform.Rotate(0, speed * maxRotationSpeed * Time.deltaTime, 0);
    }

    public override void RotateToLookAt(Vector3 target)
    {
        //Find vector from pawn position to the target's position
        Vector3 lookVector = target - transform.position;

        // Find rotation that looks down the vector
        Quaternion lookRotation = Quaternion.LookRotation(lookVector, Vector3.up);

        // Start rotating towards the target
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, maxRotationSpeed * Time.deltaTime);
    }
}
