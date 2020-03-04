﻿using System.Collections;
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


    public float powerMod, maxAngle, minAngle;
    public RectTransform rectTransform;
    public Vector2 touch;
    public AudioSource hit;

    private float timeForFlick;
    private Vector2 startDragPos, endDragPos;
    private Vector3 flickDir;
    private Touch touchCtrl;
    private bool grounded, holdingFinger;
    private Rigidbody rgbd;
    private AngleBarController angleBar;
    private ScoreController scoreThing;
    //To get the current number of the angleBar use angleBar.GetAngle();

    void Awake()
    {
        angleBar = GameObject.Find("Angle Bar").GetComponent<AngleBarController>();
        scoreThing = GameObject.Find("Score").GetComponent<ScoreController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        //rgbd.freezeRotation = true;
        if(minAngle == 0f)
            minAngle = 25f;
        if(maxAngle == 0f)
            maxAngle = 55f;
        timeForFlick = 0f;
        grounded = true;
        holdingFinger = false;
        startDragPos = Vector2.zero;
        endDragPos = Vector2.zero;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded && rgbd.velocity.magnitude < 3.5 && rgbd.velocity.magnitude > .0001)
        {
            rgbd.velocity = Vector3.zero;
            Vector3 direction = transform.position - Camera.main.transform.position;
            Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, newRotation, .05f);
            Debug.Log("ADADD");
        }

        if (holdingFinger)
            timeForFlick += 1f;

    }

    public void OnDragBegin(BaseEventData eventData)
    {
        timeForFlick = 0f;
        if(grounded)
            holdingFinger = true;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, ((PointerEventData)eventData).position, null, out startDragPos);
        Debug.Log("BEGIN");
        scoreThing.PlusStroke(1);
    }

    public void OnDragEnd(BaseEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, ((PointerEventData)eventData).position, null, out endDragPos);
        if (grounded)
        {
            holdingFinger = false;
            //Vector2 angleVec = (endDragPos - startDragPos);
            float angle = minAngle + timeForFlick;
            if (angle > maxAngle)
                angle = maxAngle;
            Debug.Log("Angle: " + angle);
            timeForFlick = 0f;
            Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            flickDir = Quaternion.AngleAxis(angle, Vector3.left) * forward;

            //Debug.Log(flickDir * (endDragPos - startDragPos).magnitude * powerMod);
            float forceMod = ((endDragPos - startDragPos).magnitude / 5) / timeForFlick;

            Vector3 force = flickDir * (endDragPos - startDragPos).magnitude / 5;
            force.y = Mathf.Abs(force.y);

            rgbd.AddForce(force, ForceMode.Impulse);
            hit.Play();
        }
        else
        {
            rgbd.AddForce((endDragPos - startDragPos)/2f);
            //hit.Play();
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
