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
        holdingFinger = false;
        startDragPos = Vector2.zero;
        endDragPos = Vector2.zero;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Values.Paused)
        {
            if (grounded && rgbd.velocity.magnitude < 3.5 && rgbd.velocity.magnitude > .0001)
            {
                rgbd.velocity = Vector3.zero;
                Vector3 direction = transform.position - Camera.main.transform.position;
                //Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.up);
                //transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, newRotation, 1f);
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

                hit.Play();
                Values.Strokes++;
            }
            else
            {
                rgbd.AddForce((endDragPos - startDragPos) / 2f);
            }
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
