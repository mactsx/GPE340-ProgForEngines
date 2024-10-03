using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        // Calculate the new location of the camera
        Vector3 newLocation = new Vector3(target.position.x, target.position.y + distance, target.position.z);

        // Move towards the new position
        transform.position = Vector3.MoveTowards(transform.position, newLocation, speed * Time.deltaTime);

        // Look at the target
        transform.LookAt(target.position, target.forward);
    }
}
