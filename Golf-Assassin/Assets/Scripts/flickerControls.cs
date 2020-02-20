using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flickerControls : MonoBehaviour
{
    public float powerMod;

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
        grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            touchCtrl = Input.GetTouch(0);
            Vector2 start, flickDirection;
            if (touchCtrl.phase == TouchPhase.Began && grounded)
            {
                //detected touch.
                start = touchCtrl.position;
                Debug.Log("Touched");
            }

            if(touchCtrl.phase == TouchPhase.Moved)
            {

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
}
