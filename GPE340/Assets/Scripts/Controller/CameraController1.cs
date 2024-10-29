using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{
    public float sensitivity;
    public bool showCursor;

    private float rotationX;
    private float rotationY;
    private Vector3 cameraRotation;


    // Start is called before the first frame update
    void Start()
    {
        if (!showCursor)
            Cursor.visible = false;

        cameraRotation = new Vector3(rotationX, rotationY, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

        // Update the x and ys for the camera
        rotationX = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        cameraRotation.x = rotationX;
        cameraRotation.y = rotationY;

        gameObject.transform.localEulerAngles = cameraRotation;

    }
}
