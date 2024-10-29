using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeam : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Color color = Color.red;
    public float lifespan = 0.1f;
    public float width = 0.5f;
    private LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        // Get the line renderer
        lr = GetComponent<LineRenderer>();

        // Set color
        lr.startColor = color;
        lr.endColor = color;

        // Set width
        lr.startWidth = width;
        lr.endWidth = width;

        // Set start and end points
        Vector3[] points = {startPoint, endPoint};
        lr.SetPositions(points);

        // Destroy self after lifespan
        Destroy(gameObject, lifespan);
    }

    
}
