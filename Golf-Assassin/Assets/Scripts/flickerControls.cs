﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class flickerControls : MonoBehaviour
{
    public float powerMod;
    public RectTransform rectTransform;
    public Vector2 touch;

    private Vector2 startDragPos, endDragPos;
    private Vector3 flickDir;
    private Touch touchCtrl;
    private bool grounded;
    private Rigidbody rgbd;
    private AngleBarController angleBar;
    //To get the current number of the angleBar use angleBar.GetAngle();

    void Awake()
    {
        angleBar = GameObject.Find("Angle Bar").GetComponent<AngleBarController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        //rgbd.freezeRotation = true;
        grounded = true;
        startDragPos = Vector2.zero;
        endDragPos = Vector2.zero;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Old Method
        //if(Input.touchCount > 0)
        //{
        //    touchCtrl = Input.GetTouch(0);
        //    Debug.Log("ADADAD");
        //    Vector2 start, flickDirection;
        //    if (touchCtrl.phase == TouchPhase.Began && grounded)
        //    {
        //        //detected touch.
        //        start = touchCtrl.position;
        //        Debug.Log("Touched");
        //    }

        //    if(touchCtrl.phase == TouchPhase.Moved)
        //    {

        //    }
        //}
        //Debug.Log(rgbd.velocity.magnitude);
            if (grounded && rgbd.velocity.magnitude < 3.5 && rgbd.velocity.magnitude > .0001)
            {
                rgbd.velocity = Vector3.zero;
                Vector3 direction = transform.position - Camera.main.transform.position;
                Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, newRotation, .05f);
                Debug.Log("ADADD");
            }

    }

    public void OnDragBegin(BaseEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, ((PointerEventData)eventData).position, null, out startDragPos);
        Debug.Log("BEGIN");
    }

    public void OnDragEnd(BaseEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, ((PointerEventData)eventData).position, null, out endDragPos);
        if (grounded)
        {
            //flickDir = endDragPos - startDragPos;
            //float angle = angleBar.GetAngle();
            //flickDir += Vector3.forward;
            //flickDir *= powerMod;
            //rgbd.AddForce(flickDir, ForceMode.Impulse);
            //grounded = false;
            //Debug.Log("END");
            //flickDir = Vector3.forward;
            flickDir = Quaternion.AngleAxis(angleBar.GetAngle(), Vector3.left) * Vector3.forward;
            Debug.Log(flickDir * (endDragPos - startDragPos).magnitude * powerMod);
            rgbd.AddForce((flickDir * (endDragPos - startDragPos).magnitude)/5, ForceMode.Impulse);

        }
        else
        {
            rgbd.AddForce((endDragPos - startDragPos).normalized*powerMod);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GroundSurface"))
        {
            grounded = true;
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
