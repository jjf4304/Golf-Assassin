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

    float maxCameraVerticalAngle = .2f;



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

        //Set the direction to look at to the line between the ball and the player
        Vector3 direction = transform.position - Camera.main.transform.position;

        //Get a rotation to look in that direction (passing in forward and up)
        Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.up);

        //Rotate the camera to look in that direction 
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, newRotation, .05f);

        //Get a vector for the opposite of the direction that the ball is moving in
        Vector3 inverseVeloc = myBody.velocity.normalized * -1;
        
        //Set the y of that vector to be zero (flattening the vertical angle)
        //inverseVeloc.y = 0f;
        inverseVeloc.y = Mathf.Clamp(inverseVeloc.y , 0, maxCameraVerticalAngle);

        //Prepare to raycast
        RaycastHit hit;

        //Get the distance between the camera and the ball // currentDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
        currentDistance = direction.magnitude;
        

        //If the camera is far away enough from the golf ball (so that the camera hopefully doesn't go inside
        if ( currentDistance > minDistance)
        {
            //Try raycasting. If we DONT hit anything (which should be most of the time):
            //if (!Physics.Raycast(transform.position, inverseVeloc, out hit, cameraDistance))
            //{

                //Place the camera at the expected position
                lerpPosition = transform.position + inverseVeloc * (cameraDistance + .01f);

                lerpPosition.y = Camera.main.transform.position.y;

            //}
            //else
            //{   
                //Otherwise, place the camera at the position of the object we hit so that it doesn't go inside a wall.
                //lerpPosition = hit.point;
            //}

            //Get the distance between the camera and the position we want to lerp to
            //float lerpDistance = Vector3.Distance(Camera.main.transform.position, lerpPosition);

            //Set the position of the camera to move to 
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, lerpPosition, myBody.velocity.magnitude * followPercentage) ;//Vector3.Lerp(Camera.main.transform.position, lerpPosition, .1f);
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x,  transform.position.y ,Camera.main.transform.position.z), followPercentage);   
            //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, lerpPosition, .5f);

        }
        //else
        //{   
            //move away from the golf ball
            //Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, transform.position, -.3f);
        //}

        Camera.main.transform.LookAt(transform.position);
    }

    

    void RotateCamera()
    {
        switch (currentTurn)
        {
            case Turn.Left:
                Camera.main.transform.Translate(Vector3.right * Time.deltaTime * 10);
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x,  transform.position.y ,Camera.main.transform.position.z), 3 * followPercentage );           
                Camera.main.transform.LookAt(transform);
                break;
            case Turn.Right:
                Camera.main.transform.Translate(-1 * Vector3.right * Time.deltaTime * 10);
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x,  transform.position.y ,Camera.main.transform.position.z), 3 * followPercentage);           
                Camera.main.transform.LookAt(transform);

                
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
