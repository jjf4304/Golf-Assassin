using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Rigidbody myBody;
    Rigidbody cameraBody;

    public float cameraDistance = 10;
    public float followPercentage = .2f;
    public float minDistance = 3;
    Vector3 lerpPosition = Vector3.zero;

    public enum Turn {Left, Right, Stop};
    public Turn currentTurn = Turn.Stop;

    bool stopCamera;
    float currentDistance;


    // Start is called before the first frame update
    void Start()
    {
        //Save our rigidbody so we can use it later.
        myBody = GetComponent<Rigidbody>();
        stopCamera = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateCamera();

        //Camera.main.transform.Rotate(Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up));
        //Camera.main.transform.LookAt(transform.position);

        Vector3 direction = transform.position - Camera.main.transform.position;
        Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.up);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, newRotation, .05f);

        Vector3 inverseVeloc = myBody.velocity.normalized * -1;
        
        //Reduce the effect of y in moving the camera.
        inverseVeloc.y *= 0f;

        RaycastHit hit;

        currentDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
        //If the camera is far away enough from the golf ball (so that the camera hopefully doesn't go inside
        if ( currentDistance > minDistance)
        {
            //Try raycasting. If we DONT hit anything (which should be most of the time):
            if (!Physics.Raycast(transform.position, inverseVeloc, out hit, cameraDistance))
            {

                //Place the camera at the expected position
                lerpPosition = transform.position + inverseVeloc * (cameraDistance + .01f);

            }
            else
            {   
                //Otherwise, place the camera at the position of the object we hit so that it doesn't go inside a wall.
                lerpPosition = hit.point;
            }

            float lerpDistance = Vector3.Distance(Camera.main.transform.position, lerpPosition);

            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, lerpPosition, myBody.velocity.magnitude * followPercentage) ;//Vector3.Lerp(Camera.main.transform.position, lerpPosition, .1f);


        }
        else
        {   
            //move away from the golf ball
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, transform.position, -.1f);
        }
    }

    void RotateCamera()
    {
        switch (currentTurn)
        {
            case Turn.Left:
                Camera.main.transform.LookAt(transform);
                Camera.main.transform.Translate(-1 * Vector3.right * Time.deltaTime * 5);
                break;
            case Turn.Right:
                Camera.main.transform.LookAt(transform);
                Camera.main.transform.Translate(Vector3.right * Time.deltaTime * 5);
                break;
        }
    }

    public void TurnLeft()
    {
        currentTurn = Turn.Left;
    }

    public void TurnRight()
    {
        currentTurn = Turn.Right;
    }

    public void StopTurn()
    {
        currentTurn = Turn.Stop;
    }
}
