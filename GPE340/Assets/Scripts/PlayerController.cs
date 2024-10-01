using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    // Update is called once per frame
    protected override void Update()
    {
        // Call parent update
        base.Update();
    }

    protected override void MakeDecsions()
    {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        moveVector = Vector3.ClampMagnitude(moveVector, 1);

        pawn.Move(moveVector);
    }
}
