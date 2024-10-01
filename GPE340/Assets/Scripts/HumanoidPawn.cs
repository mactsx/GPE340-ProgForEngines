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
        direction *= maxMoveSpeed;

        animator.SetFloat("Forward", direction.z);
        animator.SetFloat("Right", direction.x);
    }
}
