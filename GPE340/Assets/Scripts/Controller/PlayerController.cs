using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    public bool isUsingMouseRotation;
    private float speed;
    public int lives = 3;

    public bool pauseGame;

    // Update is called once per frame
    protected override void Update()
    {
        if (pauseGame == true)
        {
            GameManager.instance.isPaused = true;
        }
        else
            GameManager.instance.isPaused = false;

        // Exit early if paused
        if (GameManager.instance.isPaused)
            return;

        
        // Call parent update
        base.Update();
    }

    protected override void MakeDecsions()
    {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        moveVector = Vector3.ClampMagnitude(moveVector, 1);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = pawn.maxSprintSpeed;
        }
        else
        {
            speed = pawn.maxMoveSpeed;
        }

        pawn.Move(moveVector, speed);

        // Rotate the pawn based on the cameraRotation axis
        pawn.Rotate(Input.GetAxis("CameraRotation"));

        
        // Find the correct rotation
        // If using the mouse
        if (isUsingMouseRotation)
        {
            // Create a ray from the mouse to the direction the camera is facing
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a plane at the player's feet
            Plane bottomPlane = new Plane(Vector3.up, pawn.transform.position);

            // Find where the ray and the plane intersect and how far down the ray it is
            float distanceToIntersection;

            if (bottomPlane.Raycast(mouseRay, out distanceToIntersection) )
            {
                // Find the intersection
                Vector3 intersection = mouseRay.GetPoint(distanceToIntersection);

                // Rotate pawn to look at that intersection point
                pawn.RotateToLookAt(intersection);
            }
            else
            {
                Debug.Log("Camera is not looking at the ground - no intersection point to look at");
            }
        }
        else
        {
            //Rotate the pawn based on CameraRotation axis
            pawn.Rotate(Input.GetAxis("CameraRotation"));
        }


        // Events for firing weapons
        if (Input.GetButtonDown("Fire1"))
        {
            pawn.weapon.OnPrimaryAttackBegin.Invoke();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            pawn.weapon.OnPrimaryAttackEnd.Invoke();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            pawn.weapon.OnSecondaryAttackBegin.Invoke();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            pawn.weapon.OnSecondaryAttackEnd.Invoke();
        }
    }
}
