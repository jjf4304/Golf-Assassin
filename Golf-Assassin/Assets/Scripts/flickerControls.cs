using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class flickerControls : MonoBehaviour
{

    public float powerMod, maxAngle, minAngle, inAirRotateSpeed, tempAngle;
    public RectTransform rectTransform;
    public Vector2 touch;
    public AudioSource hit;
    public Transform tracker;
    public LineRenderer myLine;

    private float timeForFlick;
    private Vector2 startDragPos, endDragPos;
    private Vector3 flickDir, turnToDir;
    private Touch touchCtrl;
    private bool grounded, holdingFinger, stoppedMoving;
    private Rigidbody rgbd;
    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        //rgbd.freezeRotation = true;
        if (minAngle == 0f)
            minAngle = 25f;
        if (maxAngle == 0f)
            maxAngle = 55f;
        timeForFlick = 0f;
        holdingFinger = false;
        startDragPos = Vector2.zero;
        endDragPos = Vector2.zero;
        turnToDir = Vector3.zero;
        myLine = GetComponent<LineRenderer>();
        counter = 50;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If the game isn't paused
        if (!Values.Paused)
        {
            //Check and see if the ball is on the ground and is slowing to a stop
            if (grounded && rgbd.velocity.magnitude < 2)
            {
                counter++;

                //Stop the ball after a slowdown
                if (counter > 50)
                {
                    stoppedMoving = true;
                    rgbd.velocity = Vector3.zero;
                    Vector3 direction = transform.position - Camera.main.transform.position;
                    //Set the rotation to be forward of the camera
                    transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, Camera.main.transform.forward, Time.deltaTime * 10f, 0.0f));
                }
            }
            else if(grounded && rgbd.velocity.magnitude >= 2)
            {
                //Ball hasn't slowed enough
                counter = 0;
            }
            else if (!grounded )
            {
                //If the ball is in the air, take the rotation from a flick and rotate the ball's velocity towards that direction.
                Vector3 tempVelocity = rgbd.velocity;
                tempAngle *= .9f;
                Quaternion tempRot = Quaternion.AngleAxis(Time.deltaTime * tempAngle, Vector3.up);
                rgbd.velocity = tempRot * rgbd.velocity;

            }
            //Increment how long user is flicking for
            if (holdingFinger)
                timeForFlick += 1f;
        }
    }

    //If the game isn't paused. set the beginning of the drag vector position to where the touch event happens on the canvas rectangle
    public void OnDragBegin(BaseEventData eventData)
    {
        if (!Values.Paused)
        {
            timeForFlick = 0f;
            if (grounded)
                holdingFinger = true;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, ((PointerEventData)eventData).position, null, out startDragPos);
        }
    }

    //End of drag case for flicking.
    //First case is if the ball is on the ground and contains initial hit logic
    //Second case is for if the ball is in the air and contains midair flick logic 
    public void OnDragEnd(BaseEventData eventData)
    {
        if (!Values.Paused)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, ((PointerEventData)eventData).position, null, out endDragPos);
            //If the ball is on the ground and not moving
            if (grounded && stoppedMoving)
            {
                stoppedMoving = false;
                holdingFinger = false;
                //Vector2 angleVec = (endDragPos - startDragPos);
                //Get the angle to hit the ball at from the min angle and how long the drag was held for. 
                //If result is larger than maxAngle, set to maxAngle
                float angle = minAngle + timeForFlick;
                if (angle > maxAngle)
                    angle = maxAngle;
                timeForFlick = 0f;
                //Get a forwads direction for the hit to go in.
                Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                flickDir = Quaternion.AngleAxis(angle, Camera.main.transform.right) * forward;

                //Set the amount of force to hit the ball at based on how long the flick was.
                float forceMod = (startDragPos - endDragPos).magnitude / 20f;
                if (forceMod > 50f)
                {
                    forceMod = 50;
                }

                Vector3 force = flickDir * forceMod;
                //Make sure the force is upwards
                force.y = Mathf.Abs(force.y);

                //Add force to the ball
                rgbd.AddForce(force, ForceMode.Impulse);
                rgbd.AddTorque(Vector3.Cross(force, Vector3.up), ForceMode.Impulse);

                //Play the hit sfx and increase stroke count
                hit.Play();
                Values.Strokes++;
            }
            //Case for if the ball is midair when a flick is ended
            else if(!grounded)
            {
                // if drag is in the -x dir, rotate left, else rotate right
                Vector2 dragDir = endDragPos - startDragPos;
                //dragDir /= 200f;
                float dist = dragDir.magnitude / 100f;

                //Set the angle to rotate by based on what direction the flick was in
                if (dragDir.x < 0)
                {
                    tempAngle = -inAirRotateSpeed*dist;
                }
                else
                {
                    tempAngle = inAirRotateSpeed*dist;
                }


            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        turnToDir = Vector3.zero;
        if (collision.gameObject.CompareTag("GroundSurface"))
        {
            grounded = true;
            turnToDir = Vector3.zero;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("GroundSurface"))
        {
            grounded = false;
        }
    }

}
