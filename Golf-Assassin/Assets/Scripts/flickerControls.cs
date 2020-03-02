using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class flickerControls : MonoBehaviour
{
    public float powerMod;
    public RectTransform rectTransform;
    public Vector2 touch;

    private float timeForFlick;
    private Vector2 startDragPos, endDragPos;
    private Vector3 flickDir;
    private Touch touchCtrl;
    private bool grounded;
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
        grounded = true;
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

    }

    public void OnDragBegin(BaseEventData eventData)
    {
        timeForFlick = Time.time;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, ((PointerEventData)eventData).position, null, out startDragPos);
        Debug.Log("BEGIN");
        scoreThing.MinusScore(1);
    }

    public void OnDragEnd(BaseEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, ((PointerEventData)eventData).position, null, out endDragPos);
        if (grounded)
        {
            timeForFlick = Time.time - timeForFlick;

            Vector2 angleVec = (endDragPos - startDragPos);
            float angle = angleVec.y / 10;
            Debug.Log(angle);

            Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            flickDir = Quaternion.AngleAxis(angle, Vector3.left) * forward;

            //Debug.Log(flickDir * (endDragPos - startDragPos).magnitude * powerMod);
            float forceMod = ((endDragPos - startDragPos).magnitude / 5) / timeForFlick;

            Vector3 force = flickDir * (endDragPos - startDragPos).magnitude / 5;
            force.y = Mathf.Abs(force.y);

            rgbd.AddForce(force, ForceMode.Impulse);
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
