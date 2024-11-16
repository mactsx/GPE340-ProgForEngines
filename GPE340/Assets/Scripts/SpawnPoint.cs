using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private float gizmoBoxHeight = 1; 
    private float gizmoBoxWidth = 0.5f; 

    public void OnDrawGizmos()
    {
        // Create a transparent colo
        Color boxColor = Color.cyan;
        boxColor.a = 0.5f;
        // Set our gizmos to that color
        Gizmos.color = boxColor;

        // Edit the box position to be above ground
        Vector3 boxPosition = transform.position;
        boxPosition += Vector3.up * (gizmoBoxHeight / 2);
        Vector3 boxSize = new Vector3(gizmoBoxWidth, gizmoBoxHeight, gizmoBoxWidth);
        Gizmos.DrawCube(boxPosition, boxSize);

        // Set the gizmo color to red for our ray that shows direction
        Gizmos.color = Color.red;
        // And draw the ray in the direction of our spawn
        Gizmos.DrawRay(boxPosition, transform.forward);
    }
}
