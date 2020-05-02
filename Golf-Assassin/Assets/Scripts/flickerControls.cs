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
    private bool grounded, holdingFinger;
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
        if (!Values.Paused)
        {
            //Debug.Log(rgbd.velocity.magnitude);
            if (grounded && rgbd.velocity.magnitude < 2)
            {
                counter++;

                if (counter > 50)
                {
                    rgbd.velocity = Vector3.zero;
                    Vector3 direction = transform.position - Camera.main.transform.position;
                    transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, Camera.main.transform.forward, Time.deltaTime * 10f, 0.0f));
                }
            }
            else if(grounded && rgbd.velocity.magnitude >= 2)
            {
                counter = 0;
            }
            else if (!grounded )
            {
                Vector3 tempVelocity = rgbd.velocity;
                tempAngle *= .9f;
                Quaternion tempRot = Quaternion.AngleAxis(Time.deltaTime * tempAngle, Vector3.up);
                rgbd.velocity = tempRot * rgbd.velocity;

                if (holdingFinger)
                {
                    myLine.SetWidth(1f, 1f);
                    myLine.SetPosition(0, transform.position);
                    myLine.SetPosition(1, transform.position + Vector3.up*10f);
                }
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
        Debug.Log("EVENT END LOG");
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
                Vector2 dragDir = endDragPos - startDragPos;
                Debug.Log("END OF DRAG");
                //dragDir /= 200f;
                float dist = dragDir.magnitude / 100f;

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
