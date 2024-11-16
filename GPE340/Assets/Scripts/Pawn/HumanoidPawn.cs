using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidPawn : Pawn
{
    private Animator animator;
    public Transform weaponAttachPoint;

    // Start is called before the first frame update
    public override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
    }

    public override void Move(Vector3 direction, float speed)
    {
        // Convert the direction from world space to local space
        //direction = transform.InverseTransformDirection(direction);

        direction *= speed;
        
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

    public override void EquipWeapon (Weapon weaponToEquip)
    {
        // Unequip any previous weapon
        UnequipWeapon();
        // Instantiate weapon as a child of the attachment point
        Debug.Log(weaponToEquip + " has been equipped");
        weapon = Instantiate(weaponToEquip, weaponAttachPoint) as Weapon;
        weapon.transform.localPosition = Vector3.zero;
        weapon.owner = this;
    }

    public override void UnequipWeapon ()
    {
        weapon.owner = null;

        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }
        
        // Make sure weapon is null
        weapon = null;
    }


    public void OnAnimatorMove()
    {
        // After the animation, use root motion to move the game object
        transform.position = animator.rootPosition;
        transform.rotation = animator.rootRotation;

        // If the controller has a NavMeshAgent
        AIController aiController = controller as AIController;
        if (aiController != null)
        {
            // Set the nav agent to understand movement from the animator
            aiController.agent.nextPosition = animator.rootPosition;
        }
    }

    public void OnAnimatorIK()
    {
        // If there is no weapon, IK does not matter
        if (!weapon)
        {
            // Set IK weights to 0
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);

            // Break out of function
            return;
        }

        // Set Right Hand IK
        if (weapon.RightHandIKTarget)
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, weapon.RightHandIKTarget.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, .8f);
            animator.SetIKRotation(AvatarIKGoal.RightHand, weapon.RightHandIKTarget.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, .8f);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
        }

        // Set Left Hand IK
        if (weapon.LeftHandIKTarget)
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.LeftHandIKTarget.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, .8f);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.LeftHandIKTarget.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, .8f);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
        }
    }
}
