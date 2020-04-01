using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class flickerControls : MonoBehaviour
{

    /* Explanation:
     * Player starts flick, and how long the hold the flick determines the launch angle
     * Player ends flick and how long the flick was determines the launch power
     * Ball is launched directly ahead at that angle and power
     * in Air: Each flick moves the ball slightly in the direction of the flick.
     * 
     */


    public float powerMod, maxAngle, minAngle, inAirRotateSpeed;
    public RectTransform rectTransform;
    public Vector2 touch;
    public AudioSource hit;
    public Transform tracker;

    private float timeForFlick;
    private Vector2 startDragPos, endDragPos;
    private Vector3 flickDir, turnToDir;
    private Touch touchCtrl;
    private bool grounded, holdingFinger;
    private Rigidbody rgbd;

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

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Values.Paused)
        {
            tracker.position = transform.position;
            Debug.Log(grounded);
            if (grounded && rgbd.velocity.magnitude < 3.5)
            {
                Debug.Log("ADADADAD");
                rgbd.velocity = Vector3.zero;
                Vector3 direction = transform.position - Camera.main.transform.position;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, Camera.main.transform.forward, Time.deltaTime * 10f, 0.0f));
                tracker.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, Camera.main.transform.forward, Time.deltaTime * 10f, 0.0f));
            }
            else if (!grounded && turnToDir != Vector3.zero)
            {
                Quaternion rotateFrom = tracker.rotation;
                Quaternion rotateTo = Quaternion.Euler(tracker.forward + turnToDir);

                tracker.rotation = Quaternion.Lerp(rotateFrom, rotateTo, Time.deltaTime * inAirRotateSpeed);

                rgbd.velocity = Vector3.Lerp(rgbd.velocity, tracker.forward * rgbd.velocity.magnitude, Time.deltaTime);
                turnToDir = Vector3.zero;

               // transform.rotation = Quaternion.Lerp(rotateFrom, rotateTo, Time.deltaTime * inAirRotateSpeed);
                //rgbd.velocity += transform.forward;
                //turnToDir = Vector3.zero;
            }

            if (holdingFinger)
                timeForFlick += 1f;
        }
    }

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

    public void OnDragEnd(BaseEventData eventData)
    {
        if (!Values.Paused)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, ((PointerEventData)eventData).position, null, out endDragPos);
            if (grounded)
            {
                holdingFinger = false;
                //Vector2 angleVec = (endDragPos - startDragPos);
                float angle = minAngle + timeForFlick;
                if (angle > maxAngle)
                    angle = maxAngle;
                timeForFlick = 0f;
                Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                flickDir = Quaternion.AngleAxis(angle, Camera.main.transform.right) * forward;

                float forceMod = (startDragPos - endDragPos).magnitude / 20f;
                if (forceMod > 50f)
                {
                    forceMod = 50;
                }

                Vector3 force = flickDir * forceMod;
                force.y = Mathf.Abs(force.y);

                rgbd.AddForce(force, ForceMode.Impulse);
                rgbd.AddTorque(Vector3.Cross(force, Vector3.up), ForceMode.Impulse);

                hit.Play();
                Values.Strokes++;
            }
            else
            {
                // if drag in -x dir, rotate left, else rotate right
                Vector3 dragDir = endDragPos - startDragPos;
                //Quaternion turnRotation;
                //Quaternion veloRotation = Quaternion.Euler(rgbd.velocity);
                if (dragDir.x < 0)
                {
                    turnToDir = new Vector3(0, -15, 0);
                }
                else
                {
                    turnToDir = new Vector3(0, 15, 0);
                }

                //Debug.Log(turnRotation);

                //transform.rotation = Quaternion.Lerp(transform.rotation, dir, Time.time * .1f)

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
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
